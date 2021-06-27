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

And I wasn't happy with the result, so I decided to do it again and now add few more features.

NPC, patrols between a few waypoints. If a player walks into his range and it's in front of the NPC, it shoots a raycast in the direction of the player.. If the player is hit, the NPC walks to that position where he last saw the player. Then he does a 360 degree turnaround and returns back to patrolling. At any time, if a player walks into its range, the state is changed to "Chasing" and the player is either followed, if in range and in line of sight.

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

I recorded demo that shows all the features available and you can find it there: <a href="https://youtu.be/UZbOzIOV_SU" target="_blank"> Youtube demo </a>


<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.
