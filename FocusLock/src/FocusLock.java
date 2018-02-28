import IO.Encryption;
import IO.FileIO;
import IO.WebController;
import Scenes.*;

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
import javafx.scene.control.CheckBox;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.image.ImageView;
import javafx.scene.layout.AnchorPane;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;
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
	// Laver et nyt AnchorPane og tilføjer Controls til den
	AnchorPane root; 
	// Laver et objekt til at opbevarer controls i
	Scene scene;
	// Laver en ny instant af WebController class med hjemmeside som argument
	WebController Wc = new WebController();
	// En string som kommer til at opbevare alle bruger dataerne
	String userData;
	// En boolean som holder styr på om man er logget ind eller ej
	Boolean loggedIn = false;
	
	@Override
	public void start(Stage primaryStage)
	{	
		//*LOGIN CONTROLS*
		// Definere controls til Login skærm
		TextField usernameField = new TextField();
		ImageView logo = new ImageView("res/Images/BegebaLogo.png"); // Mappe skal sikkert ændres til en undermappe under /res
		PasswordField passwordField = new PasswordField();
		Label emailLabel = new Label("Email");
		Label passLabel = new Label("Adgangskode");
		Button submit = new Button("Login");
		CheckBox saveCredsBox = new CheckBox("Gem Oplysninger");
		//*LOGIN CONTROLS*
		
		//*MAIN CONTROLS*
		// Definere controls til main skærm
		Button timeButton = new Button("TID");
		Button calendarButton =  new Button("KALENDER");
		Button externButton = new Button("EKSTERN");
		ImageView helpImage =  new ImageView("res/Images/help.png");
		ImageView logoImage =  new ImageView("res/Images/Logo_NoText.png");
		AnchorPane interfacePane = new AnchorPane();
		Rectangle interfaceRectangle = new Rectangle();
		Label userLabel = new Label("REPLACE ME!");
		ImageView settingsImage = new ImageView("res/Images/settings-icon.png");
		//*MAIN CONTROLS*
		
		// Laver et nyt objekt af login class
		Login login = new Login();
		
		// Laver et nyt objekt af main class
		Main main = new Main();
		
		// Får data fra en fil ved hjælp af FileIO class
		FileIO io = new FileIO(System.getProperty("user.home") + "/Desktop/FL-Data.json");
		
		// Laver en ny instant af Encryption class
		Encryption enc = new Encryption();
		
		// Omdanner den liste FileIO class laver til en observable array
		lines = FXCollections.observableArrayList(io.ReadJsonFile());			
		
		// Tilføjer login scenen til root
		root = login.GenerateScene(usernameField, logo, passwordField, emailLabel, passLabel, submit, saveCredsBox);
		
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
		
		// Action Handler for login knappen
		submit.setOnAction(new EventHandler<ActionEvent>() {
			@Override
			public void handle(ActionEvent event) {
				// Læser data fra hjemmesiden gevet som argument
				System.out.println(enc.digestOnly(passwordField.getText()));
				System.out.println(enc.generateHASH(passwordField.getText()));
				userData = Wc.ReadWebsiteData("https://www.focuslock.dk/php/permRequest.php?email=" + usernameField.getText() + "&pass=" + enc.generateHASH(passwordField.getText()));
				// Deler hjemmeside dataerne op i en String array
//				String[] splitWebData = userData.split(";");
//				if("granted".equals(splitWebData[0]))
//				{
//					loggedIn = true;
//					System.out.println("Hej " + splitWebData[4] + ", dit ID er: " + splitWebData[3] + ". Din klasse er: " + splitWebData[2] + ", og permission er: " + splitWebData[1]);
//					
//					// Skifter scenen til main scenen og tilføjer controls
//					root = main.GenerateScene(timeButton, calendarButton, externButton, logoImage, helpImage, interfacePane, interfaceRectangle, userLabel, settingsImage);
//					scene = new Scene(root, 665, 399);
//					userLabel.setText(splitWebData[4]);
//					primaryStage.setScene(scene);
//					
//					// Tilføjer brugeren til at være online i databasen
//					Wc.ReadWebsiteData("https://www.focuslock.dk/php/addUser.php?id=" + splitWebData[3]);
//				}
			}
			
		});
		
		// Laver vinduet
		scene = new Scene(root, 454, 221);
		primaryStage.setScene(scene);
		primaryStage.setResizable(false);
		// Loader CSS'en
		scene.getStylesheets().add("res/main.css");
		primaryStage.show();
	}

	@Override
	public void stop()
	{
		// Hvis brugeren er logget ind skal man slette brugeren for at være online
		if(loggedIn)
		{
			// Sletter brugeren for at være online i databasen
			Wc.ReadWebsiteData("https://www.focuslock.dk/php/removeUser.php?id=" + userData.split(";")[3]);
		}
	}
	
	public static void main(String[] args)
	{
		launch(args);
	}
}