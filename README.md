# ConnectK Curiosities
I was very curious after a recent discussion around a coding problem, as to whether some assumptions I had made were correct. So I set about attempting to scientifically prove and disprove my thoughts, perform the code I wrote and to see which boundary conditions would break it.

## The Test

### The Problem
I was asked to write a method that would calculate whether a player had won a simpler version of Connect4. The simplicities and clarifications after questions posed on the requirements presented were as follows:

1. That the game has only a single player.
2. That there was only one colour of token at hand.
3. That the game is being played in only one direction: horizontally in a single dimension.
4. That the available slots for the tokens could be limitless horizontally.

### My Solution

I was dissatisfied with the elegance of my solution in the time allotted, but from memory, I have recreated the implementation here (also correcting a syntactical mistake with `System.Linq.Range` vs `Skip` & `Take`), and refer to it in this codebase as the Implementation Type enumerator value of: boolArrayWithPositionAwareness. I knew that I needed to be efficient with my storage and I knew that I could gain significant efficiences in search by virtue of positional awareness of the move just made by the player.

### My Assumptions

My assumptions were as follows:
- That an array of boolean values consistent with stack memory usage would be a more efficient use of storage at scale vs a string or complex type stored to heap.
- That there would be a more efficient way of querying the data at hand, but I couldn't grasp it in the time allotted.

## Method
I set about thinking through the alternative means of storage that could be used to contra-test my approach, and settled on a simple, flat `string` format, and an array of bytes. The former to be able to complete this in a single, brute-force operation; the latter to test an alternative technique with a `byte[]` array, as I felt that having a Type closer to the compiler and it's resulting machine-code would provide performance benefits.

I also began to think through the types of tests that I wanted to run and how I may perform them. This resulted in tests focusing on extending the boundaries of the data in question and the extreme directions it could be taken.

### Implementation Types
The types of implementation for this storage can be found here in an Enumerator, but the approaches are:
- Bool Array With Positional Awareness
- Bool Array, No Positional Awareness
- Flat String With Position Awareness
- Flat String, No Position Awareness
- Byte Pattern Search

### Run Types
The types of runs being tested to exercise the boundary conditions are stored here in an Enumerator, but the runs are:
- Control (the approach I took at interview)
- Plane Extension (extending the horizontal plane significantly)
- Plane & Match Extension (extending both the horizontal plane and the requirement for matches grouped together)
- Plane Extremity (extending the horizontal plane to a very high degree to purposefully stress the code)
- Plane & Match Extremity (extending the plane and pattern to improbable levels)

### Execution
Once built out in a very simplistic and procedural fashion, I defined the runs to be 10x, discarding the first two as junk due to JIT - Just In Time, compilation warm-up and using the remaining 8.

## Results
The results should be considered relative only to each other and are measured in Ticks of the `System.Diagnostics.StopWatch` on my development machine. Regardless, they were very interesting and were as follows:
```
==================================
SUMMARY OF AVERAGES IN Control TESTS
----------------------------------
🟠 boolArrayWithPositionAwareness: 30
🟢 boolArrayNoPositionRestrictor: 2
🔴 flatStringWithPositionAwareness: 38
🟠 flatStringNoPositionRestrictor: 7
🟠 bytePatternSearch: 10
```
```
==================================
SUMMARY OF AVERAGES IN PlaneExtension TESTS
----------------------------------
🔴 boolArrayWithPositionAwareness: 52
🟢 boolArrayNoPositionRestrictor: 5
🟠 flatStringWithPositionAwareness: 38
🟠 flatStringNoPositionRestrictor: 7
🟠 bytePatternSearch: 14
```
```
==================================
SUMMARY OF AVERAGES IN PlaneAndMatchExtension TESTS
----------------------------------
🟠 boolArrayWithPositionAwareness: 691
🟠 boolArrayNoPositionRestrictor: 417,710
🟢 flatStringWithPositionAwareness: 213
🔴 flatStringNoPositionRestrictor: 663,997
🟠 bytePatternSearch: 650,533
```
```
==================================
SUMMARY OF AVERAGES IN PlaneExtremity TESTS
----------------------------------
🔴 boolArrayWithPositionAwareness: 58
🟢 boolArrayNoPositionRestrictor: 6
🟠 flatStringWithPositionAwareness: 52
🟠 flatStringNoPositionRestrictor: 9
🟠 bytePatternSearch: 15
```
```
==================================
SUMMARY OF AVERAGES IN PlaneAndMatchExtremity TESTS
----------------------------------
🟠 boolArrayWithPositionAwareness: 72,205
🟠 boolArrayNoPositionRestrictor: 42,218,364
🟢 flatStringWithPositionAwareness: 18,135
🔴 flatStringNoPositionRestrictor: 65,934,199
🟠 bytePatternSearch: 64,696,109
```
```
==================================
*SUMMARY OF AVERAGES IN ALL TESTS*
----------------------------------
🟠 boolArrayWithPositionAwareness: 14,607
🟠 boolArrayNoPositionRestrictor: 8,527,217
🟢 flatStringWithPositionAwareness: 3,695
🔴 flatStringNoPositionRestrictor: 13,319,644
🟠 bytePatternSearch: 13,069,336
```
## Conclusions
### How My Solution Fared
My initial assumptions around efficiencies of storage on the stack with my smallest possible data type of `bool` held up okay*(ish)*. What became clear, was that as stressors - such as a much wider plane of data, was introduced, the inefficiencies were exposed related to how I was searching the array using `Skip` & `Take` to retrieve the local range of the move. Although the removal of the positional awareness performed exceptionally well as a simpler iteration of the `bool[]` array to assess the win as the plane spread horizontally, as soon as the Match (K) pattern increased in volume, both `bool[]` approaches began to perform poorly.
### The String Approach
Simplifying of the data source into a simple `string` came into its own as stressors ramped up. This was the most surprising result, imho. My assumption around heap usage was unfounded, as by far this was the most performant solution, although a terrible developer experience for the developers manipulating the string to represent the board movements.
### The Byte Pattern Search
This was by far the syntactical king of the solutions being tested, yet performed poorly in this implementation. I do still feel that there is an avenue of efficiency opportunity here though, in manipulating and searching the byte array, specifically with other lower-level languages closer to the compiler, such as C++. Use of the `System.Linq` namespace could be the downfall of this particular approach I have taken.
### The Winner Overall
I was surprised by how much of an effect the positional awareness actually had on the tests as the stressors of plane and match size ramped up. I believed that the string-based approach with a straight `indexOf` search with no positional awareness of the plane might perform better than it did at scale. However, the string approach was the answer to performance, although significant mitigations would have to occur for the developers using the function in order for it to be in any way usable. 

Although the `bool[]` was developer friendly and it could be employed via a `Nullable` or `struct` to introduce a third condition for each placement via the stressor of an additional player, it falls down once a third player is introduced. This does not occur in the case of the `String` or the `byte[]` array. One of these latter two approaches with variance would have been a better answer on the day.
### Alternative Approaches
There are far more efficient approaches at the stressor scale we've looked at today. Those include, but are not limited to storage of only pertinent information, i.e. where tokens are active, as in a real-world scenario, we would likely have a win long before we need to put all of the empty space into storage. Additionally, further testing of byte manipulation would I am sure result in a far better performance result for the pattern matching. 
### Acknowledgements
Thank you to Wolff for the interesting question, and developer relations insight and thank you to Eve for reaching out to me. It's been a very exciting experience to date and my desire to solve bigger problems and help others cannot be represented anywhere better than at Google. Thank you again for your time and insight.
