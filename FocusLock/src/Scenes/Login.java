/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Scenes;

import javafx.scene.control.Button;
import javafx.scene.control.CheckBox;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.image.ImageView;
import javafx.scene.layout.AnchorPane;
import javafx.scene.text.Font;

/**
 *
 * @author InsertName
 */
public class Login {
	
	public AnchorPane GenerateScene(TextField usernameField, ImageView logo, PasswordField passwordField, Label emailLabel, Label passLabel, Button submit, CheckBox saveCredsBox)
	{
		// Laver Parent til controls
		AnchorPane root = new AnchorPane();
		root.prefHeight(221.0);
		root.prefWidth(454.0);	
		
		// Laver TextFiel til brugernavn/Email		
		usernameField.setLayoutX(59.0);
		usernameField.setLayoutY(98.0);
		
		// Laver ImageView, til vores logo	
		logo.setFitHeight(60.0);
		logo.setFitWidth(404.0);
		logo.setLayoutX(14.0);
		logo.setLayoutY(14.0);
		logo.setPickOnBounds(true);
		logo.setPreserveRatio(true);
		
		// Laver PasswordField til adgangskode	
		passwordField.setLayoutX(59.0);
		passwordField.setLayoutY(158.0);
		
		// Laver Label så brugeren kan se hvor de skal skrive Email		
		emailLabel.setLayoutX(106.0);
		emailLabel.setLayoutY(74.0);
		emailLabel.setFont(Font.font(17.0));
		
		// Laver Label så brugeren kan se hvor de skal skrive adgangskode		
		passLabel.setLayoutX(82.0);
		passLabel.setLayoutY(133.0);
		passLabel.setFont(Font.font(17.0));
		
		// Laver Button til at logge ind med		
		submit.setLayoutX(345.0);
		submit.setLayoutY(139.0);
		submit.setMnemonicParsing(false);
		submit.setPrefSize(83, 39);
		
		// Laver CheckBox, så man kan bestemme om man vil gemme sine oplysninger
		saveCredsBox.setLayoutX(330.0);
		saveCredsBox.setLayoutY(183.0);
		saveCredsBox.setMnemonicParsing(false);
		// TODO: Save Creds
		
		// Tilføjer alle controls til AnchorPane
		root.getChildren().addAll(usernameField, logo, passwordField, emailLabel, passLabel, submit, saveCredsBox);
		
		return root;
	}
	    
}
