Project Description
Background
o A Traveling Salesperson Problem (TSP) is an NP-complete problem. A salesman is given a list of cities and a cost to travel between each pair of cities (or a list of city locations). The salesman must select a starting city and visit each city exactly one time and return to the starting city. His problem is to find the route (also known as a Hamiltonian Cycle) that will have the lowest cost. (See http://www.tsp.gatech.edu for more info)

Problem
o Solve an instance of a TSP problem by using a variant of the greedy heuristic
o Build tour by starting with one vertex, and inserting vertices one by one.
o Always insert vertex that is closest to an edge already in tour.
o Data for each problem will be supplied in a .tsp file (a plain text file).

Solved by splitting the map of cities at y = split value, use the right and left most
cities on each side of the split point as end points. The cities between the end points
are inserted between the cities whose edge's center point is the closest to the city being inserted.
The paths created above and below the split point are combined to create a tour and the process repeats
with different split points to generate multiple tours, and the shortest tour is returned.
AI Reports Link: https://drive.google.com/drive/folders/0By--ZxTFsr-mZDR5dUt6bElHajg?usp=sharing