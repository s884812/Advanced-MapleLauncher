# Advanced-MapleLauncher
An advanced custom MapleStory launcher for private servers.

### About
Instead of sharing a large file sized archive that contains all the required files to play a MapleStory private server, just simply share this launcher that is 31 KB minimum in size. This launcher will check the files of the user's MapleStory folder and will download the necessary files that the user does not have or is not updated based off of a small XML configuration file you configure and upload to your site. This will keep your players updated with the most current files, allowing less problems to occur and keeping your players updated with the newest content.

**Demonstration video:** https://www.youtube.com/watch?v=rESO3j3x_08

### Configuring the source
Just edit the `Launcher.cs` and replace `http://linktoyourxml.com/launcher.xml` to the URL of where your XML is uploaded. 

For example, if you uploaded your XML at `http://kappastory.com/launcher.xml`, the code should look like this:
```csharp
private static string _xmlURL = "http://kappastory.com/launcher.xml"; // The URL to the XML file you uploaded.
```

### How to set up XML

The launcher uses the XML file to grab all the information it needs to function. If the file sizes stated in the XML is different than
the file sizes in the user's MapleStory folder, it will delete the old files in the user's MapleStory and download the updated files using
the download link that is provided in the XML.

**Your XML should be formatted exactly like below. Just edit the settings between the <> blocks.**

**The sizes are in bytes. The WZ files that are set default in the XML below are clean v83 WZ files with the correct file sizes and working direct download links.**

```xml
<serverinfo>
	<servername>Server Name Here</servername> <!-- Your server name. -->
	<websiteurl>http://linktoyourwebsite.com</websiteurl> <!-- The URL to the website. -->
	<forumurl>http://linktoyourforum.com</forumurl> <!-- The URL to the forum. -->
	<voteurl>http://linktothevotingpage.com</voteurl> <!--The URL to the voting page. -->

	<exprate>1000x</exprate> <!-- The server's EXP rate. -->
	<mesorate>500x</mesorate> <!-- The server's meso rate. -->
	<droprate>5x</droprate> <!-- The server's drop rate.-->
</serverinfo>

<news> <!-- Recent news goes on top. Goes from newer -> older from top to bottom. -->
	<title>More Recent News</title> <!-- The title of the news. -->
	<message>This is a more recent news.</message> <!-- The message of the news. -->

	<title>Old News</title>
	<message>This is an older news.</message>
</news>

<downloads> <!-- Sizes are in bytes. The download link has to be a direct download link. -->
	<client_link>http://clientdownloadlink.com</client_link> <!-- The direct download link to the file. -->
	<client_size>0000</client_size> <!-- The size in bytes of the file. -->
	<client_name>MapleClient.exe</client_name> <!-- The client's file name. ( Include the .exe ) -->

	<base_link>https://www.dropbox.com/s/zi7oaam63gqearh/Base.wz?dl=1</base_link> <!-- The direct link to the file -->
	<base_size>6540</base_size> <!-- The size in bytes of the file. -->

	<character_link>https://www.dropbox.com/s/2h8e59h566kanwa/Character.wz?dl=1</character_link>
	<character_size>186792151</character_size>

	<effect_link>https://www.dropbox.com/s/irxtpocq87h1ss2/Effect.wz?dl=1</base_link>
	<effect_size>63334965</effect_size>

	<etc_link>https://www.dropbox.com/s/2ko4uad5bydpo63/Etc.wz?dl=1</etc_link>
	<etc_size>1201101</etc_size>

	<item_link>https://www.dropbox.com/s/dvoub5hu52238v9/Item.wz?dl=1</item_link>
	<item_size>18361778</item_size>

	<list_link>https://www.dropbox.com/s/7bfjubjiksc9vpp/List.wz?dl=1</list_link>
	<list_size>13336</list_size>

	<map_link>https://www.dropbox.com/s/n02zawj3ynq8nce/Map.wz?dl=1</map_link>
	<map_size>635444895</map_size>

	<mob_link>https://www.dropbox.com/s/9cwdodqpfsjwa4k/Mob.wz?dl=1</mob_link>
	<mob_size>479665103</mob_size>

	<morph_link>https://www.dropbox.com/s/i54huwfo0e9k5zz/Morph.wz?dl=1</morph_link>
	<morph_size>6204606</morph_size>

	<npc_link>https://www.dropbox.com/s/48vxli6nxpl46hq/Npc.wz?dl=1</npc_link>
	<npc_size>51137866</npc_size>

	<quest_link>https://www.dropbox.com/s/0rpv7weuyyuzqcd/Quest.wz?dl=1</quest_link>
	<quest_size>5971537</quest_size>

	<reactor_link>https://www.dropbox.com/s/6ygr0ph1zc9ktsl/Reactor.wz?dl=1</reactor_link>
	<reactor_size>54133811</reactor_size>

	<skill_link>https://www.dropbox.com/s/rsoq7wz4ilj404w/Skill.wz?dl=1</skill_link>
	<skill_size>76874780</skill_size>

	<sound_link>https://www.dropbox.com/s/lpmg415a8owtzvh/Sound.wz?dl=1</sound_link>
	<sound_size>363261964</sound_size>

	<string_link>https://www.dropbox.com/s/mnjpuwomnul05da/String.wz?dl=1</string_link>
	<string_size>3251564</string_size>

	<tamingmob_link>https://www.dropbox.com/s/k9v4iwdoz48x8g0/TamingMob.wz?dl=1</tamingmob_link>
	<tamingmob_size>797</tamingmob_size>

	<ui_link>https://www.dropbox.com/s/bdxywid1cdw2xxm/UI.wz?dl=1</ui_link>
	<ui_size>28259933</ui_size>
</downloads>
```
