<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
      <a href="#architecture">Architecture</a> 
      <ul> 
        <li><a href="#enemy-system">Enemy System</a> </li> 
        <li> <a href="#enemy-ai">Enemy AI</a> </li> 
        <li><a href="#player-combat">Player Combat</a></li> 
        <li><a href="#player-progression">Player Progression</a></li> 
      </ul>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project
This project was created as a university assignment for my Software Architecture course. The focus of the assignment was on learing about and visualising our code structure using UML diagrams. In addition to it, the requirements called for a dungeon crawler type game containing multiple enemies and a boss. Each enemy has their own type of attack and AI behaviour, while the boss combines all three types and dynamically switches during battle. 

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

* [![Unity][Unity.img]][Unity-url]
* [![C#][C#.img]][C#-url]
* [UnityNavMesh][Unity NavMesh-url]
* [Draw.io][Draw-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Architecture

### Enemy System
**_EnemyController_** is a runtime component on every enemy. It uses **_EnemyData_** asset, which holds variables such as health, damage, speed and XP. Movement effects such as freezes and knockback are given to **_EnemyNavMeshController_**.

For proper reaction to enemy event, I am using **observers**. **_EnemyObserver_** is the abstrack class that contains **_OnEnemyCreated_** (Initiated when an enemy is spawned; uses **_EnemyData_**), **_OnEnemyHit_** (What happens when an enemy is hit) and **__OnEnemyDied_** (What happens when an enemy dies). **_EnemyUIObserver_** is resposible for updating the UI elements linked to the enemy, like its health number and bar, while **_ItemXpObserver_** spawns the XP orbs, dropped by the enemies. **_EnemySpawner_** and **_EnemyPrefab_** handle populating the dungeon. 

<img width="858" height="476" alt="Screenshot 2026-08-24 145057" src="https://github.com/user-attachments/assets/19b211ce-f336-440b-9d40-bdc69a6f57d8" />

### Enemy AI
The enemy behaviour uses **finite state machine**. **_State_** holds a list of **_Transitions_**. Each transition pairs a condition with the state it leads to. **_EnemyFSM_** creates the behaviour of every enemy - **_MeleeBehaviour_**, **_RangeBehaviour_**, **_AuraBehaviour_** and **_BossBehaviour_**, which reuses the other 3 behaviours. 

<img width="856" height="473" alt="Screenshot 2026-08-24 145728" src="https://github.com/user-attachments/assets/4227ef9e-8d68-41ed-85b3-767be18c402d" />
<img width="857" height="479" alt="Screenshot 2026-08-24 145739" src="https://github.com/user-attachments/assets/e76a87c9-eb3f-468f-b09f-ae437ba9da98" />

### Player Combat 
**_Attacks_** is an abstract script that holds information regarding every attack such as the name, cooldown, type, description and needed skill tokens to unlock the attack. The attacks that the player can unlock are **_FireballAttack_**, **_FreezeAttack_**, **_GravityPushAttack_**, each with 3 levels. The only exception is the first level of the fireball attack. This one is given to the player by default. 

<img width="863" height="482" alt="Screenshot 2026-08-24 145625" src="https://github.com/user-attachments/assets/1dd44c7a-4e95-4bc2-8cf9-53b10efdfc1a" />

### Player Progression
**_PlayerStats_** hold information regarding the different statistics of the player such as their health, level, XP and skill tokens. The tokens are used in the **skill tree**, where the player can unlock different attacks. 

The player can collect XP and level up by killing enemies and collection XP orbs or by completing quests. 

<img width="849" height="476" alt="Screenshot 2026-08-24 145358" src="https://github.com/user-attachments/assets/14c51ded-75a8-461f-ba59-f48bcfad7f05" />

<!-- GETTING STARTED -->
## Getting Started

### Prerequisites
* Unity 6000.2.6f1 or later

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->
## Usage
**Controls:**
* W / A / S / D - movement
* Mouse - look around
* LMB - cast an attack
* 1, 2, 3 - switch between attacks
* E - open inventory
* U - open skill tree
* ESC - open pause menu 

<p align="right">(<a href="#readme-top">back to top</a>)</p>

[Unity.img]: https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white
[Unity-url]: https://unity.com/
[C#.img]: http://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=unity&logoColor=white
[C#-url]: https://learn.microsoft.com/en-us/dotnet/csharp/
[Unity NavMesh-url]: https://docs.unity3d.com/ScriptReference/AI.NavMesh.html
[Draw-url]: https://www.drawio.com/
