/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package scenes;

import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.Timeline;
import javafx.application.Application;
import javafx.beans.binding.Bindings;
import javafx.beans.binding.StringBinding;
import javafx.beans.property.ObjectProperty;
import javafx.beans.property.SimpleObjectProperty;
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

/**
 *
 * @author Apollo
 */
public class loginScene extends Application
{
	public static void main (String[] args)
	{
		Application.launch(args);
	}
	
	@Override
	public void start(Stage primaryStage)
	{
		// Laver Parent til controls
		AnchorPane root = new AnchorPane();
		root.prefHeight(500.0);
		root.prefWidth(800.0);
		
		// Laver vinduet
		primaryStage.getIcons().add(new Image("res/grap/fav.png"));
		final Scene scene = new Scene(root, 800, 500);
		primaryStage.setScene(scene);
		primaryStage.setResizable(false);
		
		// Laver ImageView, til vores logo
		ImageView logo = new ImageView("res/grap/iconText.png");
		logo.setPickOnBounds(true);
		logo.setPreserveRatio(true);
//		logo.setFitHeight(88.0);
		logo.setFitWidth(600.0);
		
		StackPane stack = new StackPane();
		stack.getChildren().add(logo);
		stack.translateXProperty().bind(scene.widthProperty().subtract(stack.widthProperty()).divide(2));
		stack.translateYProperty().bind(scene.heightProperty().subtract(stack.heightProperty()).divide(5));
		
		// Laver TextField til brugernavn/Email
		TextField usernameField = new TextField();
		usernameField.setLayoutX(59.0+150);
		usernameField.setLayoutY(98.0+75+50);
		
		// Laver PasswordField til adgangskode
		PasswordField passwordField = new PasswordField();
		passwordField.setLayoutX(59.0+150);
		passwordField.setLayoutY(158.0+75+50);
		
		// Laver Label så brugeren kan se hvor de skal skrive Email
		Label emailLabel = new Label("Email");
		emailLabel.setLayoutX(106.0+102);
		emailLabel.setLayoutY(74.0+75+50);
		
		// Laver Label så brugeren kan se hvor de skal skrive adgangskode
		Label passLabel = new Label("Adgangskode");
		passLabel.setLayoutX(82.0+128);
		passLabel.setLayoutY(133.0+75+50);
		
		// Laver Button til at logge ind med
		Button submit = new Button("LOGIN");
		submit.setId("btn-default-lg");
		submit.setLayoutX(345.0+150);
		submit.setLayoutY(139.0+55+30);
		submit.setMnemonicParsing(true);
		//submit.setPrefSize(83, 39);
		// TODO: Action Handler
		
		// Laver CheckBox, så man kan bestemme om man vil gemme sine oplysninger
		CheckBox saveCredsBox = new CheckBox("Gem Oplysninger");
		saveCredsBox.setLayoutX(330.0+150);
		saveCredsBox.setLayoutY(183.0+55+50);
		saveCredsBox.setMnemonicParsing(false);
		// TODO: Save Creds
		
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
		root.getChildren().add(emailLabel);
		root.getChildren().add(passLabel);
		root.getChildren().add(submit);
		root.getChildren().add(saveCredsBox);
		root.getChildren().add(stack);
		
		// Loader CSS'en
		scene.getStylesheets().add("res/main.css");
		primaryStage.show();
	}
}
