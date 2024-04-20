# BRUTALISTICK VR Official Modding Framework
## Getting Started
1. Install Unity on your PC, the current version used by the game is 2023.3.24f1 LTS.
2. Create a new project, preferably with the Universal 3D template, although you can just manually add the Universal RP package from the Package Manager after launching the project.
3. Download the BrutalistickVRModdingFramework.unitypackage and import it by dragging and dropping it into the open project or just double clicking the file.
4. Make sure that under the Edit\Project Settings\Graphics Settings tab the Scriptable Render Pipeline Settings are set to URP-Brutalistick.
   ![image](https://github.com/Kubunautilus/BrutalistickVR_ModFramework/assets/167565930/b2be9307-ca0f-4d35-8e71-698ba63abe9e)


## Creating a Melee Weapon
Open the **ModdingScene** from the folder **Scenes**. There are two example weapons, one is the club hammer from the base game recreated using the modding framework with blunt settings. The second is a new long dagger created from scratch using the framework with sharp settings. You can always reference these or build a new weapon using these as a base.
1. Create a new folder (this will be referenced as _ModWeaponFolder_ further in the tutorial) where you'll be placing all the assets that are needed for your mod to work. Everything that the weapon prefab uses should be contained in this folder. Examples of this setup in the included project are the folders LongDagger and ClubHammer. It is more memory friendly to create multiple weapons in one mod if they have at least one overlapping asset (Texture, Material, Model, Audio), otherwise it'll load both instances of the asset into memory.
2. Create a Game Object in the scene where you'll be placing all the assets. The required components for this are a **Melee Weapon Wrapper** script and a **Rigidbody** (Collision Detection should be set to Continuous Dynamic and Mass should be set according to the weapon weight).
3. Add colliders for the weapon. To use different tags you have to use different child game objects for the colliders. It would be best if you could approximate the shape using only Sphere, Capsule and Box colliders but if there is a complex shape that can't be properly represented with those, for example the shovel head in the base game, you can use a Convex Mesh collider. In order of collision physics calculation complexity, it's Sphere > Capsule > Box >>> Mesh. For denting to work properly, you should use the tags Blunt or Handle.
4. To support slicing for sharp weapons, you should add a **SlicePlane** from the Modding folder as a direct child of the weapon. You can temporarily enable the Mesh Renderer on the **SlicePlane** object to see where you should place it but make sure to disable it again once it's in place. You also need to add **Trigger Colliders** on the side(s) of the blade where slicing can take place. Only those body parts can be sliced that are in the trigger at the time of impact. Try not to make the trigger too large as that can cause slices even if you hit them with the side of the blade.
5. To support stabbing for sharp weapons, add the **StabLine** prefab from the Modding folder as a direct child of the weapon and position **StabTip** and **StabBase** to the tip of the stabber and the base of the stabber respectively. You also have to add an extra collider (preferably sphere or capsule) to the tip of the weapon on a new child game object and add that to the **Melee Weapon Wrapper** script's **Stabbing Colliders**.
6. For grabbing, add the **GrabPoints** prefab and the **GrabTopAndBottom** prefab from the **Modding** folder as direct childs of the weapon. Position the **GrabTopAndBottom** children to the top and bottom of the handle, this will allow grabbing and sliding the weapon anywhere between those two points. The center point between those will be used as the grab point when you remove the item from a socket or force grab it. You will also have to add the colliders that will be used for detecting a grab to the **Melee Weapon Wrapper** script's **Colliders For Detecting Grabs**, otherwise the weapon can't be picked up. This can be bypassed by placing the grabbable collider on the same game object as the **Melee Weapon Wrapper**.
7. Create a new **MeleeScriptableObject** in the _ModWeaponFolder_
   ![image](https://github.com/Kubunautilus/BrutalistickVR_ModFramework/assets/167565930/eb45e51e-4d29-4114-a587-cb9924c1436c)
8. The first thing you'll want to do is find and enter your Steam User ID. The easiest way to do this is to use [SteamIDFinder](https://www.steamidfinder.com/) and look under steamID3, the numbers after "U:1:" are what we're looking for. Please note that this is a different number from your steamID64 that's displayed in the URL of your profile. This is to help prevent other users from downloading your mods and reuploading them under their own names.

![image](https://github.com/Kubunautilus/BrutalistickVR_ModFramework/assets/167565930/e48bdb96-7d69-4a5a-92d6-5b964dfd5766)

9. Enter all the other parameters, the default values ought to be a pretty good starting point.
10. The **Hit Sounds Type** governs whether the weapon is made out of one material or two. For example, a crowbar would use a single material and only have one set of impact sound effects. A hammer with a wooden handle would have two, one for the metal impact and the other for wooden impact sounds. However, the **Double Material** type limits the amount of different tags you can have to two. A third tag will not produce impact sounds.
11. Add the new **MeleeScriptableObject** to the **Melee Weapon Wrapper** script's **Melee Weapon SO** slot on your game object.
12. For materials, make sure you use the **Universal Render Pipeline/Lit** shader. This will be automatically converted to a shader that's supported by the game when the mod is loaded in. For now, transparent materials are not supported, Occlusion and Height maps are not taken into account and neither are Detail Inputs or Advanced Options.
13. Now that you've finished setting up the weapon, drag it into the _ModWeaponFolder_ to create a prefab. Now you're ready to start bundling your mod up.
14. Select the whole folder in the asset browser and in the bottom right under the inspector, there is a menu called AssetBundle.![image](https://github.com/Kubunautilus/BrutalistickVR_ModFramework/assets/167565930/2007ba4a-acc3-484b-b979-988986248e25)
15. Click on that and select New...![image](https://github.com/Kubunautilus/BrutalistickVR_ModFramework/assets/167565930/005a68dc-18b6-479c-9634-762f25d166a9)
16. Name your mod. It is highly recommended to put a little personal touch on it, like your username, since Asset Bundles with the same name will fail to load. For example, if you have two separate mods that are both named shortswordmod, it'll only load the first one in and the second will give an error. This issue can be minimized by naming your mod shortswordbykubunautilus.
17. Select Assets/Build Mod AssetBundles
    
![image](https://github.com/Kubunautilus/BrutalistickVR_ModFramework/assets/167565930/818581de-09df-4806-a5a4-cc5feca5fe08)

19. It'll now build your mod Asset Bundles and place them into a folder called CreatedModBundles. You can right click on that folder and click Show in Explorer. Now create a new folder with the name of your mod, this is the name that will show up on the Upload to Steam Workshop menu in-game. Place the created mod bundle files inside (for example clubhammermod and clubhammermod.manifest) the folder. Now findyour BRUTALISTICK VR directory and navigate to BRUTALISTICK VR/BrutalistickVR_Data/StreamingAssets/Mods and place the created mod folder inside. The mod files must be placed there as a folder, loose Asset Bundles will not be loaded. Before uploading it to Steam Workshop, you should add a thumbnail to that folder, either "preview.png" or "preview.jpg" (case-sensitive). Thumbnails can't be added manually from the workshop page but other screenshots can be, and the description and the name can be changed at will.
20. Now the weapon should show up on your weapon wall in-game.

## Uploading to Steam Workshop

1. The first step is to add the launch argument "-modding" to your install.

![image](https://github.com/Kubunautilus/BrutalistickVR_ModFramework/assets/167565930/3ea301fd-7abb-47fa-8d07-f9565576fb94)

2. Now you can launch the game and in the Main Hub, there should be a new button on the Main Options menu called Enter Mod Workshop.
3. If you entered your Steam ID correctly, the mods should show up in the place where the mission menu does in the base game. It might take some time to load it in, especially if you've already uploaded a mod before.
4. If you've already uploaded a mod before, it'll show two options next to the mod, Upload as New or Upload as Update. Otherwise only the Upload as New button will be shown.
### Upload as New
Uploading as New will create a new workshop page for your item with the preview file thumbnail, with the folder name as a title and with visibility set to private. It may appear to not work at first but once it finishes uploading, it'll open the Steam Overlay to the freshly created workshop page. Once it does, you can start changing the name, description and adding screenshots. Once you're ready, you can make the workshop page public to allow others to download it. This is probably easier to do outside of your headset but the overlay exists as confirmation that the upload succeeded.
### Upload as Update
Clicking Upload as Update will then give you the next page where you can choose from your previously uploaded mods. This will update it with the new files in the mod folder and will replace the preview image if one is supplied. Once this is completed, it will open the Steam Overlay to the updated workshop page.
