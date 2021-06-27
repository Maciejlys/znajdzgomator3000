<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

I was responsible for making simple NPC that would follow the player after they got into its range, and you can find the first version <a href="https://github.com/Maciejlys/znajdzgomator2000">there</a>

And I wasn't happy with the result, so I decided to do it again and now I added few more features.

The idea  was simple:
* Make an NPC that patrols between waypoints
* If player walks into its range and the player is in front of the NPC, it goes to that position. If it still has line of sight then it follows the player, if not it goes to last position where the player was
* If the player is missing and NPC can't see them, it waits few seconds, turns around to check every direction and then returns to patrolling
* At any time, even when going back to patroling NPC should be able to see the player and follow him if conditions mentioned above are fulfilled.

NPC, patrols between a few waypoints. If a player walks into his range and it's in front of the NPC, it shoots a raycast in the direction of the player. If the player is hit, the NPC walks to that position where he last saw the player. Then he does a 360 degree turnaround and returns back to patrolling. At any time, if a player walks into its range, the state is changed to "Chasing" and the player is either followed, if in range and in line of sight.

I also added few indicators so its easier to see where are the waypoints and where is he currently heading.


### Built With

* Unity
* C#
* NavMesh


<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites

U will need <a href="https://unity3d.com/get-unity/download">Unity</a> on your PC

### Installation

1. Install Unity
2. Go into Unity Hub
3. Add this project
4. Click on it after its added


<!-- USAGE EXAMPLES -->
## Usage

Your map have to be baked, set your world as Navigation Static and then bake.

Your **player** needs to have these components:
* Tag "Player"

Your **NPC** needs to have these components:
* Sphere colider
* Nav mesh agent
* EnemyMovement
* Waypoints

**Sphere colider** indicates how big range of the NPC is, it will be shooting raycast as soon as player walks into its range.

**Waypoints** have to be somewhere in the game world and they have to be assigned to EnemyMovement script. If you want an indicator of where the NPC is currently heading then
make another waypoint, make it visible and then assign it to EnemyMovement.


I recorded demo that shows all the features available and you can find it there: <a href="https://youtu.be/UZbOzIOV_SU" target="_blank"> Youtube demo </a>


<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.
