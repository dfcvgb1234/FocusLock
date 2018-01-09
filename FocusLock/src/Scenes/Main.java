/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Scenes;

import javafx.geometry.Pos;
import javafx.scene.control.Button;
import javafx.scene.control.ContentDisplay;
import javafx.scene.control.Label;
import javafx.scene.image.ImageView;
import javafx.scene.layout.AnchorPane;
import javafx.scene.paint.Paint;
import javafx.scene.shape.Rectangle;
import javafx.scene.shape.StrokeType;
import javafx.scene.text.Font;

/**
 *
 * @author InsertName
 */
public class Main {
	
	public AnchorPane GenerateScene(Button timeButton, Button calendarButton, Button externButton, ImageView logo, ImageView helpImage,
					AnchorPane interfacePane, Rectangle interfaceRectangle, Label userLabel, ImageView settingsImage)
	{
		// Fonten der sidder på knapperne
		Font buttonFont = new Font("Meiryo", 18.0);
		
		// Laver et nyt Anchorpane
		AnchorPane root = new AnchorPane();
		root.setPrefSize(399.0, 665.0);
		
		// Tidsfunktions knappen
		timeButton.setLayoutX(22.0);
		timeButton.setLayoutY(109.0);
		timeButton.setMnemonicParsing(false);
		timeButton.setPrefSize(127.0, 49.0);
		timeButton.setFont(buttonFont);
		
		// Kalenderfunktions knappen
		calendarButton.setLayoutX(22.0);
		calendarButton.setLayoutY(185.0);
		calendarButton.setMnemonicParsing(false);
		calendarButton.setPrefSize(127.0, 49.0);
		calendarButton.setFont(buttonFont);
		
		// Eksternfunktions knappen
		externButton.setLayoutX(22.0);
		externButton.setLayoutY(262.0);
		externButton.setMnemonicParsing(false);
		externButton.setPrefSize(127.0, 49.0);
		externButton.setFont(buttonFont);
		
		// Et ny Anchorpane, som bliver brugt som interface
		interfacePane.setLayoutX(189.0);
		interfacePane.setLayoutY(49.0);
		interfacePane.setPrefSize(311.0, 430.0);
		
		// Border og baggrund ved interface 
		interfaceRectangle.setArcHeight(5.0);
		interfaceRectangle.setArcWidth(5.0);
		interfaceRectangle.setFill(Paint.valueOf("#9c9c9c45"));
		interfaceRectangle.setHeight(322.0);
		interfaceRectangle.setStroke(Paint.valueOf("BLACK"));
		interfaceRectangle.setStrokeType(StrokeType.INSIDE);
		interfaceRectangle.setStrokeWidth(2.0);
		interfaceRectangle.setWidth(455.0);
		interfacePane.getChildren().add(interfaceRectangle);
		
		// Label der fortæller om brugerens navn
		userLabel.setAlignment(Pos.CENTER_RIGHT);
		userLabel.setContentDisplay(ContentDisplay.CENTER);
		userLabel.setLayoutX(45.0);
		userLabel.setLayoutY(14.0);
		userLabel.setPrefSize(599.0, 27.0);
		userLabel.setFont(new Font(18));
		
		// Knappen med indstillinger
		settingsImage.setFitHeight(38.0);
		settingsImage.setFitWidth(44.0);
		settingsImage.setLayoutX(35.0);
		settingsImage.setLayoutY(333.0);
		settingsImage.setPickOnBounds(true);
		settingsImage.setPreserveRatio(true);
		
		// Knappen med hjælp
		helpImage.setFitHeight(66.0);
		helpImage.setFitWidth(38.0);
		helpImage.setLayoutX(98.0);
		helpImage.setLayoutY(333.0);
		helpImage.setPickOnBounds(true);
		helpImage.setPreserveRatio(true);
		
		// Logo
		logo.setFitHeight(106.0);
		logo.setFitWidth(127.0);
		logo.setLayoutX(33.0);
		logo.setLayoutY(3.0);
		logo.setPickOnBounds(true);
		logo.setPreserveRatio(true);
		
		// Tilføjer alle controls til Root/AnchorPane
		root.getChildren().addAll(timeButton, calendarButton, externButton, interfacePane, userLabel, settingsImage, helpImage, logo);
		
		// Retunere Root, så FocusLock klassen kan bruge den
		return root;	
	}
	
}
