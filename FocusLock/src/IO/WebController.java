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
	
	// Contructor til Class
	public WebController(String website)
	{
		// Gemmer argumentet i den globale "website" variabel
		this.website = website;
	}
	
	// Metode til at læse data fra en hjemmeside
	public void ReadWebsiteData()
	{
		try {
			// Åbner linket og henter data i en stream
			URL link = new URL(website);
			BufferedReader in = new BufferedReader(new InputStreamReader(link.openStream()));
			
			// Skriver til consolen hvad der står på hjemmesiden
			String inputLine;
			while ((inputLine = in.readLine()) != null)
			{
				System.out.println(inputLine);
			}
			in.close();
		
		// Exception handlers
		} catch (MalformedURLException ex) {
			Logger.getLogger(WebController.class.getName()).log(Level.SEVERE, null, ex);
		} catch (IOException ex) {
			Logger.getLogger(WebController.class.getName()).log(Level.SEVERE, null, ex);
		}
	}
	
}
