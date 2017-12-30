import focuslockbutton.OsCheck;
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

public class FocusLockButton extends Application
{
	@Override
	public void start(Stage primaryStage)
	{
		// Debugging
		System.out.println(System.getProperty("user.home") + "\\Desktop\\hej.txt");
		// Får data fra en fil ved hjælp af FileIO class
		FileIO io = new FileIO(System.getProperty("user.home") + "/Desktop/hej.txt");
		ObservableList<String> lines = null;

		// Sikre at der ikke sker en fejl når vi læser data fra filen
		try
		{
			// Omdanner den liste FileIO class laver til en observable array
			lines = FXCollections.observableArrayList(io.GenerateLineList());
		}
		catch (IOException ex)
		{
			Logger.getLogger(FocusLockButton.class.getName()).log(Level.SEVERE, null, ex);
		}

		// Generer et GridPane som vi kan putte controls på
		final GridPane root = new GridPane();
		final Button button = new Button("CHROME KILLER");
		// Laver et ListView som den inderholder alle de linjer fra Filen
		final ListView lv = new ListView(lines);
		button.setId("btn-default-lg");

		// Farverne på knappen hvordan den skal ændre sig i "Timeline"
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

		// Lamba function der checker om der bliver trykket på knappen
		button.setOnAction((ActionEvent event) ->
		{
			// OsCheck er fra filen OsCheck.java
			// Checker hvilken OS brugeren sidder på
			OsCheck.OSType ostype = OsCheck.getOperatingSystemType();
			
			// Debugging
			System.out.println(ostype.toString());
			
			// Switch case til forskellige commands der skal udføres alt efter hvilket OS man sidder på
			switch (ostype)
			{
				case Windows:
					try
					{
						String[] cmd = {"taskkill","/F","/IM", "chrome.exe"};
						// Kører den command der er givet ovenover, De første 3 argumenter er påkrævet!
						Runtime.getRuntime().exec(cmd);
					}
					catch (IOException ex)
					{
						Logger.getLogger(FocusLockButton.class.getName()).log(Level.SEVERE, null, ex);
					}
					break;
					
				case MacOS:
					try
					{
						String[] cmd = {"killall", "Google Chrome"};
						// Kører den command der er givet ovenover, Det første argument er påkrævet!
						Runtime.getRuntime().exec(cmd);
					}
					catch (IOException ex)
					{
						Logger.getLogger(FocusLockButton.class.getName()).log(Level.SEVERE, null, ex);
					}
					break;
					
				case Linux:
					try
					{
						// IKKE TESTET ENDNU
						button.setId("btn-default-lg-linux"); //DEBUG
						// Skulle gerne virke, men ikke testet
						String[] cmd = {"killall", "Google Chrome"};
						// Kører den command der er givet ovenover
						Runtime.getRuntime().exec(cmd);
					}
					catch (IOException ex)
					{
						Logger.getLogger(FocusLockButton.class.getName()).log(Level.SEVERE, null, ex);
					}
					break;
					
				case Other:
					// TODO: Send en besked til vores server omkring styresystem og fejl
					break;
			}
		});

		// Start hover over knappen
		button.setOnMouseEntered((MouseEvent event) ->
		{
			timeline2.stop();
			timeline.play();
		});

		// Stop hover over knappen
		button.setOnMouseExited((MouseEvent event) ->
		{
			timeline.stop();
			timeline2.play();
		});

		// Tilføjer controls til pane	
		root.add(button, 1, 0, 2, 1);
		root.add(lv, 0, 0);

		final Scene scene = new Scene(root, 420, 175);
		primaryStage.setScene(scene);
		primaryStage.setResizable(false);
		// Loader CSS'en
		scene.getStylesheets().add("focuslockbutton/main.css");
		primaryStage.show();
	}

	public static void main(String[] args)
	{
		launch(args);
	}
}