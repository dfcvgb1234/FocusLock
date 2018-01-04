import IO.Encryption;
import IO.FileIO;
import IO.WebController;

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
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
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
		
		// Laver Parent til controls
		AnchorPane root = new AnchorPane();
		root.prefHeight(221.0);
		root.prefWidth(454.0);
		
		// Laver TextFiel til brugernavn/Email
		TextField usernameField = new TextField();
		usernameField.setLayoutX(59.0);
		usernameField.setLayoutY(98.0);
		
		// Laver ImageView, til vores logo
		ImageView logo = new ImageView("res/BegebaLogo.png"); // Mappe skal sikkert ændres til en undermappe under /res
		logo.setFitHeight(60.0);
		logo.setFitWidth(404.0);
		logo.setLayoutX(14.0);
		logo.setLayoutY(14.0);
		logo.setPickOnBounds(true);
		logo.setPreserveRatio(true);
		
		// Laver PasswordField til adgangskode
		PasswordField passwordField = new PasswordField();
		passwordField.setLayoutX(59.0);
		passwordField.setLayoutY(158.0);
		
		// Laver Label så brugeren kan se hvor de skal skrive Email
		Label emailLabel = new Label("Email");
		emailLabel.setLayoutX(106.0);
		emailLabel.setLayoutY(74.0);
		emailLabel.setFont(Font.font(17.0));
		
		// Laver Label så brugeren kan se hvor de skal skrive adgangskode
		Label passLabel = new Label("Adgangskode");
		passLabel.setLayoutX(82.0);
		passLabel.setLayoutY(133.0);
		passLabel.setFont(Font.font(17.0));
		
		// Laver Button til at logge ind med
		Button submit = new Button("Login");
		submit.setLayoutX(345.0);
		submit.setLayoutY(139.0);
		submit.setMnemonicParsing(false);
		submit.setPrefSize(83, 39);
		// TODO: Action Handler
		
		// Laver CheckBox, så man kan bestemme om man vil gemme sine oplysninger
		CheckBox saveCredsBox = new CheckBox("Gem Oplysninger");
		saveCredsBox.setLayoutX(330.0);
		saveCredsBox.setLayoutY(183.0);
		saveCredsBox.setMnemonicParsing(false);
		// TODO: Save Creds
		
		
		
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
        
		// Farverne på knappen, hvordan den skal ændre sig i "Timeline"
		final Color startColor = Color.web("#F6F6F6");
		final Color endColor = Color.web("#FC6B0A");
	
		// Får farve property og gæmmer det i en color variabel
		final ObjectProperty<Color> color = new SimpleObjectProperty<>(startColor);
        
        
		final StringBinding cssColorSpec = Bindings.createStringBinding(() -> String.format("-fx-body-color: rgba(%d, %d, %d);", 
			(int) (256*color.get().getRed()),
			(int) (256*color.get().getGreen()),
			(int) (256*color.get().getBlue())), color);
        
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
        
//		// Checker når der bliver ændret i selection i ListView
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
        
		// Start hover over knappen
		submit.setOnMouseEntered((MouseEvent event) -> {
			timeline2.stop();
			timeline.play();
		});
        
		// Stop hover over knappen
		submit.setOnMouseExited((MouseEvent event) -> {
			timeline.stop();
			timeline2.play();
		});
		
		// Tilføjer controls til Anchor Pane	
		root.getChildren().add(usernameField);
		root.getChildren().add(passwordField);
		root.getChildren().add(logo);
		root.getChildren().add(emailLabel);
		root.getChildren().add(passLabel);
		root.getChildren().add(submit);
		root.getChildren().add(saveCredsBox);
		
		// Laver vinduet
		final Scene scene = new Scene(root, 454, 221);
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