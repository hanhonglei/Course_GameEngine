# Useful resolutions
1. How to quit game in editor mode:
```C#
public void Quit()
{
	#if UNITY_EDITOR
	EditorApplication.isPlaying = false;
	#else
	Application.Quit();
	#endif
}
```
----

- This project is under [GNU GENERAL PUBLIC LICENSE](https://www.gnu.org/licenses/), please check it in the root folder.
		
