package OS;
import static OS.OSCheck.detectedOS;
import java.util.Locale;
/**
 *
 * @author TheGejr
 */
public final class OSCheck
{
	public enum OSType
	{
		Windows, MacOS, Linux, Other
	};

	protected static OSType detectedOS;

	public static OSType getOperatingSystemType()
	{
		if (detectedOS == null)
		{
			String OS = System.getProperty("os.name", "generic").toLowerCase(Locale.ENGLISH);
			
			if ((OS.contains("mac")) || (OS.contains("darwin")))
			{
				detectedOS = OSType.MacOS;
			}
			else if (OS.contains("win"))
			{
				detectedOS = OSType.Windows;
			}
			else if (OS.contains("nux")) {
				detectedOS = OSType.Linux;
			}
			else
			{
				detectedOS = OSType.Other;
			}
		}
		
		return detectedOS;
	}
}