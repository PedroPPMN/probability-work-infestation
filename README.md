# probability work infestation
Developed in Unity as an academic work for a probability and statistics course in 2019.

Description:
Given an n x n matrix:
1. Feed her initially with the following individuals:
1.1. One infected, whose position is random;
1.2. A random number of immunes, whose positions are random; 1.3. A random number of pseudo immunes, whose positions are random; 1.4. The rest of the matrix cells must be filled with healthy individuals.
2. The infected individual must infect its four direct neighbors (it cannot infect diagonal neighbors)
2.1. Immunes cannot be infected;
2.2. The pseudo-immune must have a percentage of immunity, such as 30%. Thus, when the infected person tries to infect him, a random number (X) must be created that will be compared with the 30%, following the following rule: 2.2.1. If X > 30% then infect otherwise do not infect
2.3. The healthy must be infected.
3. An infection carried out by an infected individual consists of:
3.1. The infected acquire all properties of the infectant;
3.2. The infector infects its four neighbors, considering the rules of item 2 4. Only the infected individuals walk into the network, one position per update, being able to jump to any of the four direct positions.
Update 2: Ends when infected individuals 2, 3, 4, and 5 infect their neighbors and walk.
5. Deaths can occur:
5.1. Healthy or immune normally die after 10 upgrades (10 steps) or after the accident;
5.2. Pseudo-immunes die after 4 upgrades or crash;
5.3. Infected die after 3 upgrades or crashes;
6. Accidents may occur. This is before starting the next update: 6.1. Considering that each individual has a percentage of 10% of having an accident, for example;
6.2. You must scan the entire matrix, comparing a number to be created for each scanned cell (random number, Y) with the 10%. Thus, If Y< 10% then it dies, else it stays alive.
7. Births can occur.
7.1. This occurs before starting the next update. The cells that do not contain individuals due to the occurrence of deaths contain a percentage of 80% that is compared with a random number (Z). If Z<80% then it is born, if not, it is not born.
7.2. Those born can be of the following types, being chosen at random: 7.2.1. Immune;
7.2.2. Pseudo-immune;
8. At the end of each update, the system must save the following information: 8.1. Number of Immunes;
8.2. Number of pseudo-immune;
8.3. Number of infectants generated;
8.4. Number of patients;
8.5. Number of casualties;
8.6. Number of healthy;
8.7. Births.
9. The values generated in item 9 must be taken to Excel or a statistical program and described with:
9.1. Average;
9.2. Standard deviation
9.3. median
10. Check if the experiment fits a normal distribution.
11. Find the population sample that represents the population as a whole with 95% certainty.
12. With this sample, predict the behavior of the population in the next 5 time steps.
13. What are the percentages that lead the network to immunity? the death? the endemic?
