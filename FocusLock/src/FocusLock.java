import OS.OSCheck;
import FileController.FileIO;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;

import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.Timeline;
import javafx.application.Application;
import javafx.beans.binding.Bindings;
import javafx.beans.binding.StringBinding;
import javafx.beans.property.ObjectProperty;
import javafx.beans.property.SimpleObjectProperty;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.ListView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import javafx.scene.paint.Color;
import javafx.stage.Stage;
import javafx.util.Duration;

public class FocusLock extends Application
{
	// variabel der holder styr på hvilket program der skal lukkes
        String selectedProgram = "";
        //  Liste som der skal indeholde alle lines i filen
        ObservableList<String> lines;
	
	@Override
	public void start(Stage primaryStage)
	{
		// Debugging
		System.out.println(System.getProperty("user.home") + "\\Desktop\\hej.txt");
		// Får data fra en fil ved hjælp af FileIO class
		FileIO io = new FileIO(System.getProperty("user.home") + "/Desktop/hej.txt");
        
        
		// Sikre at der ikke sker en fejl når vi læser data fra filen
		try {
			// Omdanner den liste FileIO class laver til en observable array
			lines = FXCollections.observableArrayList(io.GenerateLineList());
		} catch (IOException ex) {
			Logger.getLogger(FocusLock.class.getName()).log(Level.SEVERE, null, ex);
		}
        
		// Generer et GridPane som vi kan putte controls på
		final GridPane root = new GridPane();
		// Laver en knap, med teksten "SELECT PROGRAM"
		final Button button = new Button("SELECT PROGRAM");
		// Laver et ListView som den inderholder alle de linjer fra Filen
		final ListView lv = new ListView();
		
		// Tilføjer alle elementer til ListView
		lines.forEach((line) -> {
			lv.getItems().add(line.split(";")[1]);
		});
		button.setId("btn-default-lg");
        
		// Farverne på knappen, hvordan den skal ændre sig i "Timeline"
		final Color startColor = Color.web("#F6F6F6");
		final Color endColor = Color.web("#FC6B0A");
	
		// Får farve property og gæmmer det i en color variabel
		final ObjectProperty<Color> color = new SimpleObjectProperty<>(startColor);
        
        
		final StringBinding cssColorSpec = Bindings.createStringBinding(() -> String.format("-fx-body-color: rgba(%d, %d, %d);", 
			(int) (256*color.get().getRed()),
			(int) (256*color.get().getGreen()),
			(int) (256*color.get().getBlue())), color);
		
		button.styleProperty().bind(cssColorSpec);
        
		// Anim der aktiveres ved hover over knappen
		final Timeline timeline = new Timeline(
			new KeyFrame(Duration.ZERO, new KeyValue(color, startColor)),
			new KeyFrame(Duration.seconds(.2), new KeyValue(color, endColor))
		);
        
		// Anim der aktivers når der ikke længere hoves over knappen
		final Timeline timeline2 = new Timeline(
			new KeyFrame(Duration.ZERO, new KeyValue(color, endColor)),
			new KeyFrame(Duration.seconds(.2), new KeyValue(color, startColor))
		);
        
		// Checker når der bliver ændret i selection i ListView
		lv.getSelectionModel().selectedItemProperty().addListener(new ChangeListener() {
			@Override
			public void changed(ObservableValue observable, Object oldValue, Object newValue) {
				// Debugging
				System.out.println(lv.getItems().indexOf(newValue));
                
				// Sætter selectedProgram til den liste i filen
				selectedProgram = lines.get(lv.getItems().indexOf(newValue));
				// Ændre teksten på knappen til at være hvilket program der skal lukkes
				button.setText(newValue.toString().toUpperCase() + " KILLER");

				// Debugging
				System.out.println(selectedProgram);
			}
		});
        
		// Eventhandler der checker om der bliver trykket på knappen
		button.setOnAction(new EventHandler<ActionEvent>() {
			@Override
			public void handle(ActionEvent event) {
				// OsCheck er fra filen OsCheck.java
				// Checker hvilken OS brugeren sidder på
				OSCheck.OSType ostype = OSCheck.getOperatingSystemType();

				// Debugging
				System.out.println(ostype.toString());

				// Switch case til forskellige commands der skal udføres alt efter hvilket OS man sidder på
				switch (ostype) {
					case Windows:
					try
					{
						// DEN FINDER WINDOWS KORREKT TESTET
						String[] cmd = {"taskkill","/F","/IM", selectedProgram.split(";")[0]};
						// Kører den command der er givet ovenover
						Runtime.getRuntime().exec(cmd);
					}
					catch (IOException ex)
					{
						Logger.getLogger(FocusLock.class.getName()).log(Level.SEVERE, null, ex);
					}
					break;
					case MacOS:
					try
					{
						// DEN FINDER MAC KORREKT TESTET
						String[] cmd = {"killall", selectedProgram.split(";")[1]};
						// Kører den command der er givet ovenover
						Runtime.getRuntime().exec(cmd);
					}
					catch (IOException ex)
					{
						Logger.getLogger(FocusLock.class.getName()).log(Level.SEVERE, null, ex);
					}
					break;
					case Linux:
					try
					{
						// IKKE TESTET ENDNU
						button.setId("btn-default-lg-linux");
						// Skulle gerne virke, men ikke testet
						String[] cmd = {"killall", selectedProgram.split(";")[1]};
						// Kører den command der er givet ovenover
						Runtime.getRuntime().exec(cmd);
					}
					catch (IOException ex)
					{
						Logger.getLogger(FocusLock.class.getName()).log(Level.SEVERE, null, ex);
					}
					break;
					case Other:
					// TODO: Send en besked til vores server omkring styresystem og fejl
					break;
				}

			}
		});
        
		// Start hover over knappen
		button.setOnMouseEntered(new EventHandler<MouseEvent>() {
			@Override
			public void handle(MouseEvent event) {
				timeline2.stop();
				timeline.play();
			}
		});
        
		// Stop hover over knappen
		button.setOnMouseExited(new EventHandler<MouseEvent>() {
			@Override
			public void handle(MouseEvent event) {
				timeline.stop();
				timeline2.play();
			}
		});
		
		// Tilføjer controls til pane	
		root.add(button, 1, 0, 2, 1);
		root.add(lv, 0, 0);
        
		final Scene scene = new Scene(root, 600, 175);
		primaryStage.setScene(scene);
		primaryStage.setResizable(false);
		// Loader CSS'en
		scene.getStylesheets().add("res/main.css");
		primaryStage.show();
	}

	public static void main(String[] args)
	{
		launch(args);
	}
}