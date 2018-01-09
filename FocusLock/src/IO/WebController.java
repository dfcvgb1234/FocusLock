package IO;
import java.net.*;
import java.io.*;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author IceBerg
 */
public class WebController {
	
	// Global variabel til linket
	String website;
	
	// Metode til at læse data fra en hjemmeside
	public String ReadWebsiteData(String website)
	{
		try {
			// Åbner linket og henter data i en stream
			URL link = new URL(website);
			BufferedReader in = new BufferedReader(new InputStreamReader(link.openStream()));
			
			// Skriver til consolen hvad der står på hjemmesiden
			String inputLine;
			inputLine = in.readLine();
			in.close();
			return inputLine;
		
		// Exception handlers
		} catch (MalformedURLException ex) {
			Logger.getLogger(WebController.class.getName()).log(Level.SEVERE, null, ex);
			return "ERROR";
		} catch (IOException ex) {
			Logger.getLogger(WebController.class.getName()).log(Level.SEVERE, null, ex);
			return "ERROR";
		}
	}
	
}
