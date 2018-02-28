package IO;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Base64;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author IceBerg
 */
public class Encryption {
	
	// Metode til at Hashe en string værdi
	public String generateHASH(String password)
	{
		try {
			//Salt til hashing
			String salt = "www.favs2@gmail.com";
			
			// Får de bytes der er i både password og salt
			byte[] passBytes = password.getBytes();
			byte[] saltBytes = salt.getBytes();
			
			// Hasher kodeordet til byte array
			MessageDigest digest = MessageDigest.getInstance("SHA-256");
			byte[] encodedPassByteHash = digest.digest(passBytes);
			
			// Laver ny byte array med længde på hashet kodeord og salt
			byte[] concat = new byte[encodedPassByteHash.length + saltBytes.length];
			
			// Gemmer alle bytes i concat arrayet (i den rigtige rækkefølge)
			System.arraycopy(saltBytes, 0, concat, 0, saltBytes.length);
			System.arraycopy(encodedPassByteHash, 0, concat, saltBytes.length, encodedPassByteHash.length);
			
			// Hasher concat arrayet
			byte[] hashedBytes = digest.digest(concat);
			
			// Encoder det med Base64
			byte[] encoded = Base64.getEncoder().encode(hashedBytes);
		
			// Omdanner det til en string og returner det
			return new String(encoded);
			
		// Exception handling	
		} catch (NoSuchAlgorithmException ex) {
			Logger.getLogger(Encryption.class.getName()).log(Level.SEVERE, null, ex);
			return "ERROR";
		}	
	}
	
	public String digestOnly(String password)
	{
		try {
			//Salt til hashing
			String salt = password;
			
			// Får de bytes der er i både password og salt
			byte[] passBytes = password.getBytes();
			byte[] saltBytes = salt.getBytes();
			
			// Hasher kodeordet til byte array
			MessageDigest digest = MessageDigest.getInstance("SHA-256");
			byte[] encodedPassByteHash = digest.digest(passBytes);
			
			// Laver ny byte array med længde på hashet kodeord og salt
			byte[] concat = new byte[encodedPassByteHash.length + saltBytes.length];
			
			// Gemmer alle bytes i concat arrayet (i den rigtige rækkefølge)
			System.arraycopy(saltBytes, 0, concat, 0, saltBytes.length);
			System.arraycopy(encodedPassByteHash, 0, concat, saltBytes.length, encodedPassByteHash.length);
			
			// Hasher concat arrayet
			byte[] hashedBytes = digest.digest(concat);
			
			// Omdanner de hashede bytes til en HEX string
			StringBuilder hexstring = new StringBuilder();
			for (int i = 0; i < hashedBytes.length; i++) {
				String hex = Integer.toHexString(0xff & hashedBytes[i]);
				if(hex.length() == 1) hexstring.append('0');
					hexstring.append(hex);
			}
			// Omdanner det til en string og returner det
			return hexstring.toString();
			
		// Exception handling	
		} catch (NoSuchAlgorithmException ex) {
			Logger.getLogger(Encryption.class.getName()).log(Level.SEVERE, null, ex);
			return "ERROR";
		}	
	}
	
	
	
}


