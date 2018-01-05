import IO.Encryption;
import IO.FileIO;
import IO.WebController;

import scenes.loginScene;

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
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.CheckBox;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.StackPane;
import javafx.scene.paint.Color;
import javafx.stage.Stage;
import javafx.util.Duration;
import org.json.simple.parser.JSONParser;

/**
 *
 * @author IceBerg
 */
public class FocusLock extends Application
{
	// variabel der holder styr på hvilket program der skal lukkes
        String selectedProgram = "";
        // Liste som der skal indeholde alle lines i filen
        ObservableList<String> lines;
	// JSONParser til ListView
	JSONParser parser = new JSONParser();
	
	@Override
	public void start(Stage primaryStage)
	{
//		// Laver Parent til controls
//		AnchorPane root = new AnchorPane();
//		root.prefHeight(500.0);
//		root.prefWidth(800.0);
//		
//		// Laver vinduet
//		primaryStage.getIcons().add(new Image("res/grap/fav.png"));
//		final Scene scene = new Scene(root, 800, 500);
//		primaryStage.setScene(scene);
//		primaryStage.setResizable(false);
		
		// Får data fra en fil ved hjælp af FileIO class
		FileIO io = new FileIO(System.getProperty("user.home") + "/Desktop/hej.json");
		
		// Laver en ny instant af Encryption class
		Encryption enc = new Encryption();
		// Skriver til consollen hvad der bliver returned
		System.out.println(enc.generateHASH("Chsb1234")); // Temp, skal modtage data fra passwordField
		
		// Laver en ny instant af WebController class med hjemmeside som argument
		WebController Wc = new WebController("https://www.focuslock.dk/php/permRequest.php?email=www.favs2@gmail.com&pass=" + enc.generateHASH("Chsb1234"));	
		// Læser data fra hjemmesiden gevet ovenover
		Wc.ReadWebsiteData();
		
		// Omdanner den liste FileIO class laver til en observable array
		lines = FXCollections.observableArrayList(io.ReadJsonFile());			
		
//		// Laver ImageView, til vores logo
//		ImageView logo = new ImageView("res/grap/iconText.png");
//		logo.setPickOnBounds(true);
//		logo.setPreserveRatio(true);
////		logo.setFitHeight(88.0);
//		logo.setFitWidth(600.0);
//		
//		StackPane stack = new StackPane();
//		stack.getChildren().add(logo);
//		stack.translateXProperty().bind(scene.widthProperty().subtract(stack.widthProperty()).divide(2));
//		stack.translateYProperty().bind(scene.heightProperty().subtract(stack.heightProperty()).divide(5));
//		
//		// Laver TextField til brugernavn/Email
//		TextField usernameField = new TextField();
//		usernameField.setLayoutX(59.0+150);
//		usernameField.setLayoutY(98.0+75+50);
//		
//		// Laver PasswordField til adgangskode
//		PasswordField passwordField = new PasswordField();
//		passwordField.setLayoutX(59.0+150);
//		passwordField.setLayoutY(158.0+75+50);
//		
//		// Laver Label så brugeren kan se hvor de skal skrive Email
//		Label emailLabel = new Label("Email");
//		emailLabel.setLayoutX(106.0+102);
//		emailLabel.setLayoutY(74.0+75+50);
//		
//		// Laver Label så brugeren kan se hvor de skal skrive adgangskode
//		Label passLabel = new Label("Adgangskode");
//		passLabel.setLayoutX(82.0+128);
//		passLabel.setLayoutY(133.0+75+50);
//		
//		// Laver Button til at logge ind med
//		Button submit = new Button("LOGIN");
//		submit.setId("btn-default-lg");
//		submit.setLayoutX(345.0+150);
//		submit.setLayoutY(139.0+55+30);
//		submit.setMnemonicParsing(true);
//		//submit.setPrefSize(83, 39);
//		// TODO: Action Handler
//		
//		// Laver CheckBox, så man kan bestemme om man vil gemme sine oplysninger
//		CheckBox saveCredsBox = new CheckBox("Gem Oplysninger");
//		saveCredsBox.setLayoutX(330.0+150);
//		saveCredsBox.setLayoutY(183.0+55+50);
//		saveCredsBox.setMnemonicParsing(false);
//		// TODO: Save Creds
		
		
		
//		// Tilføjer alle elementer til ListView
//		lines.forEach((String line) -> {
//			
//			// Sikrer en udvej hvis der sker en fejl
//			try {
//				// Laver et nyt JSON objekt, ud af vores JSON Parser
//				JSONObject obj = (JSONObject) parser.parse(line);
//				// Tilføjer dem til ListView
//				lv.getItems().add(obj.get("mac-linux"));
//			} catch (ParseException ex) {
//				Logger.getLogger(FocusLock.class.getName()).log(Level.SEVERE, null, ex);
//			}
//		});
        
//		// Farverne på knappen, hvordan den skal ændre sig i "Timeline"
//		final Color startColor = Color.web("#F6F6F6");
//		final Color endColor = Color.web("#FC6B0A");
//	
//		// Får farve property og gæmmer det i en color variabel
//		final ObjectProperty<Color> color = new SimpleObjectProperty<>(startColor);
//        
//        
//		final StringBinding cssColorSpec = Bindings.createStringBinding(() -> String.format("-fx-body-color: rgba(%d, %d, %d);", 
//			(int) (256*color.get().getRed()),
//			(int) (256*color.get().getGreen()),
//			(int) (256*color.get().getBlue())), color);
//        
//		// Anim der aktiveres ved hover over knappen
//		final Timeline timeline = new Timeline(
//			new KeyFrame(Duration.ZERO, new KeyValue(color, startColor)),
//			new KeyFrame(Duration.seconds(.2), new KeyValue(color, endColor))
//		);
//        
//		// Anim der aktivers når der ikke længere hoves over knappen
//		final Timeline timeline2 = new Timeline(
//			new KeyFrame(Duration.ZERO, new KeyValue(color, endColor)),
//			new KeyFrame(Duration.seconds(.2), new KeyValue(color, startColor))
//		);

//		Checker når der bliver ændret i selection i ListView
//		lv.getSelectionModel().selectedItemProperty().addListener(new ChangeListener() {
//			@Override
//			public void changed(ObservableValue observable, Object oldValue, Object newValue) {
//				// Debugging
//				System.out.println(lv.getItems().indexOf(newValue));
//                
//				// Sætter selectedProgram til den liste i filen
//				selectedProgram = lines.get(lv.getItems().indexOf(newValue));
//				// Ændre teksten på knappen til at være hvilket program der skal lukkes
//				button.setText(newValue.toString().toUpperCase() + " KILLER");
//
//				// Debugging
//				System.out.println(selectedProgram);
//			}
//		});
        
//		// Eventhandler der checker om der bliver trykket på knappen
//		button.setOnAction(new EventHandler<ActionEvent>() {
//			@Override
//			public void handle(ActionEvent event) {
//				// OsCheck er fra filen OsCheck.java
//				// Checker hvilken OS brugeren sidder på
//				OSCheck.OSType ostype = OSCheck.getOperatingSystemType();
//
//				// Debugging
//				System.out.println(ostype.toString());
//				
//				// Switch case til forskellige commands der skal udføres alt efter hvilket OS man sidder på
//				switch (ostype) {
//					case Windows:
//					try
//					{
//						// Parser vores selected program, så den kører den rigtige kommando
//						JSONObject obj = (JSONObject) parser.parse(selectedProgram);
//						// DEN FINDER WINDOWS KORREKT TESTET
//						String[] cmd = {"taskkill","/F","/IM", obj.get("windows").toString()};
//						// Kører den command der er givet ovenover
//						Runtime.getRuntime().exec(cmd);
//					}
//					catch (IOException | ParseException ex)
//					{
//						Logger.getLogger(FocusLock.class.getName()).log(Level.SEVERE, null, ex);
//					}
//					break;
//					case MacOS:
//					try
//					{
//						// Parser vores selected program, så den kører den rigtige kommando
//						JSONObject obj = (JSONObject) parser.parse(selectedProgram);
//						// DEN FINDER MAC KORREKT TESTET
//						String[] cmd = {"killall", obj.get("mac-linux").toString()};
//						// Kører den command der er givet ovenover
//						Runtime.getRuntime().exec(cmd);
//					}
//					catch (IOException | ParseException ex)
//					{
//						Logger.getLogger(FocusLock.class.getName()).log(Level.SEVERE, null, ex);
//					}
//					break;
//					case Linux:
//					try
//					{
//						// Parser vores selected program, så den kører den rigtige kommando
//						JSONObject obj = (JSONObject) parser.parse(selectedProgram);
//						// IKKE TESTET ENDNU
//						button.setId("btn-default-lg-linux");
//						// Skulle gerne virke, men ikke testet
//						String[] cmd = {"killall", obj.get("mac-linux").toString()};
//						// Kører den command der er givet ovenover
//						Runtime.getRuntime().exec(cmd);
//					}
//					catch (IOException | ParseException ex)
//					{
//						Logger.getLogger(FocusLock.class.getName()).log(Level.SEVERE, null, ex);
//					}
//					break;
//					case Other:
//					// TODO: Send en besked til vores server omkring styresystem og fejl
//					break;
//				}
//
//			}
//		});
        
//		// Start hover over knappen
//		submit.setOnMouseEntered((MouseEvent event) -> {
//			timeline2.stop();
//			timeline.play();
//		});
//        
//		// Stop hover over knappen
//		submit.setOnMouseExited((MouseEvent event) -> {
//			timeline.stop();
//			timeline2.play();
//		});
//		
//		// Tilføjer controls til Anchor Pane	
//		root.getChildren().add(usernameField);
//		root.getChildren().add(passwordField);
//		root.getChildren().add(emailLabel);
//		root.getChildren().add(passLabel);
//		root.getChildren().add(submit);
//		root.getChildren().add(saveCredsBox);
//		root.getChildren().add(stack);
//		
//		// Loader CSS'en
//		scene.getStylesheets().add("res/main.css");
//		primaryStage.show();
	}

	public static void main(String[] args)
	{
		launch(args);
	}
}