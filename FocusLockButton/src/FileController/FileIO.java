/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package FileController;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;


/**
 *
 * @author InsertName
 */
public class FileIO {
    
    Path path;
    
    List<String> lines;
    
    // Constructor til class
    public FileIO(String path)
    {
        this.lines = new ArrayList<>();
        this.path = Paths.get(path);
        
        if(!Files.exists(this.path))
        {
            try 
            {
                Files.createFile(this.path);
            } 
            catch (IOException ex) 
            {
                Logger.getLogger(FileIO.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
    }
    
    public List<String> GenerateLineList() throws IOException
    {
        lines = Files.readAllLines(path);
        return lines;
    }   
}
