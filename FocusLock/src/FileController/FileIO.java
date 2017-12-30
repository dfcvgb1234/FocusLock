package FileController;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

public class FileIO
{
	// Path til filen der skal læses fra
	Path path;

	List<String> lines;

	// Constructor til class
	public FileIO(String path)
	{
		// Laver en ny liste med alle linjerne i filen (ny fil-struktur for program filen)
		this.lines = new ArrayList<>();
		this.path = Paths.get(path);

		// Checker om filen eksistere, hvis ikke lav den
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

	// Fylder den generede liste med linjerne og retunere den
	public List<String> GenerateLineList() throws IOException
	{
		lines = Files.readAllLines(path);
		return lines;
	}   
}
