package IO;

import java.io.FileReader;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import org.json.simple.parser.JSONParser;

/**
 *
 * @author IceBerg
 */
public class FileIO
{
	// Path til filen der skal læses fra
	Path path;
    
	// Laver en liste med alle linjerne i
	List<String> lines = null;
	
	// Constructor til class
	public FileIO(String path)
	{
		// Laver en ny liste med alle linjerne i filen (ny fil-struktur for program filen)
		this.lines = new ArrayList<>();
		this.path = Paths.get(path);
        
		// Checker om filen eksistere, hvis ikke lav den
		if(!Files.exists(this.path))
		{
			// Fortæller om der er sket en fejl
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
    
	// Fylder den generede liste med linjerne og retunere den
	public List<String> GenerateLineList() throws IOException
	{
		// Læser alle linjer i filen
		for(String line : Files.readAllLines(path))
		{
			if(!line.contains("#") && !line.isEmpty())
			{
				lines.add(line);
			}
		}
				
		return lines;
	} 
	
	// Læser vores Json fil og gemmer alt dataerne i en liste
	public List<String> ReadJsonFile()
	{
		// Laver et nyt JSONParser object, som der læser JSON filen
		JSONParser parser = new JSONParser();
		try {
			// Gemmer indholdet af filen i et objekt
			Object obj = parser.parse(new FileReader(path.toString()));
			
			// Laver objektet om til et JSONObjekt
			JSONObject jObj = (JSONObject) obj;
			
			// Får den array som der hedder "programs" inde i JSON filen
			JSONArray programs = (JSONArray) jObj.get("programs");
			
			// Laver en Iterator, som der kan kører igennem alt arrayens indhold
			Iterator i = programs.iterator();
			
			// Kører igennem alt indholdet af "programs" arrayen
			while(i.hasNext())
			{
				// Laver et JSON objekt af den nuværende position
				JSONObject inOBJ = (JSONObject) i.next();
				// Tilføjer indholdet i vores lines list
				lines.add(inOBJ.toString());
			}
		}
		catch(Exception ex)
		{}
		return lines;
	}
}
