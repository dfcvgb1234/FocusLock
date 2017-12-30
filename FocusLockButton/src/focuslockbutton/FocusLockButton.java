import focuslockbutton.OsCheck;
import java.io.Console;
import java.io.IOException;
import java.util.concurrent.Callable;
import java.util.logging.Level;
import java.util.logging.Logger;

import javafx.animation.Animation;
import javafx.animation.Interpolator;
import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.RotateTransition;
import javafx.animation.Timeline;
import javafx.application.Application;
import javafx.beans.binding.Bindings;
import javafx.beans.binding.StringBinding;
import javafx.beans.property.ObjectProperty;
import javafx.beans.property.SimpleObjectProperty;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.Pane;
import javafx.scene.layout.StackPane;
import javafx.scene.paint.Color;
import javafx.scene.shape.Rectangle;
import javafx.stage.Stage;
import javafx.util.Duration;

public class FocusLockButton extends Application {

    @Override
    public void start(Stage primaryStage) {
        final StackPane root = new StackPane();
        final Button button = new Button("CHROME KILLER");
        button.setId("btn-default-lg");
		
        final Color startColor = Color.web("#F6F6F6");
        final Color endColor = Color.web("#FC6B0A");
		
        final ObjectProperty<Color> color = new SimpleObjectProperty<>(startColor);

        final StringBinding cssColorSpec = Bindings.createStringBinding(() -> String.format("-fx-body-color: rgba(%d, %d, %d);", 
                (int) (256*color.get().getRed()),
                (int) (256*color.get().getGreen()),
                (int) (256*color.get().getBlue())), color);
		
        button.styleProperty().bind(cssColorSpec);
        
        final Timeline timeline = new Timeline(
            new KeyFrame(Duration.ZERO, new KeyValue(color, startColor)),
            new KeyFrame(Duration.seconds(.2), new KeyValue(color, endColor))
        );
        
        final Timeline timeline2 = new Timeline(
            new KeyFrame(Duration.ZERO, new KeyValue(color, endColor)),
            new KeyFrame(Duration.seconds(.2), new KeyValue(color, startColor))
        );

        button.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                // OsCheck er fra filen OsCheck.java
                OsCheck.OSType ostype=OsCheck.getOperatingSystemType();
                    switch (ostype) {
                        case Windows:
                            try
                            {
                                // DEN FINDER WINDOWS KORREKT TESTET
                                Runtime.getRuntime().exec("taskkill /F /IM chrome.exe");
                            }
                            catch (IOException ex)
                            {
                                Logger.getLogger(FocusLockButton.class.getName()).log(Level.SEVERE, null, ex);
                            }
                            break;
                        case MacOS:
                            try
                            {
                                // DEN FINDER MAC KORREKT TESTET
                                button.setId("btn-default-lg-mac");
                                // Denne kommand lukker ikke Google Chrome, selvom at i mac terminal kan jeg skrive "killall 'Google Chrome' " og den lukker det.
                                Runtime.getRuntime().exec("/bin/sh killall 'Google Chrome'");
                            }
                            catch (IOException ex)
                            {
                                Logger.getLogger(FocusLockButton.class.getName()).log(Level.SEVERE, null, ex);
                            }
                            break;
                        case Linux:
                            try
                            {
                                Runtime.getRuntime().exec("/bin/bash -c killall firefox");
                            }
                            catch (IOException ex)
                            {
                                Logger.getLogger(FocusLockButton.class.getName()).log(Level.SEVERE, null, ex);
                            }
                            break;
                        case Other:
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
		
		
        root.getChildren().addAll(button);
		
        final Scene scene = new Scene(root, 300, 175);
        primaryStage.setScene(scene);
        primaryStage.setResizable(false);
        // Loader CSS'en
        scene.getStylesheets().add("focuslockbutton/main.css");
        primaryStage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }
}