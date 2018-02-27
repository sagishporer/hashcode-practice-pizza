# Hash Code 2018 practice problem solution: Pizza

[Problem definition & data files](https://bytefreaks.net/google/practice-problem-for-google-hash-code-2018)

Algorithm type: greedy. 
1. Scan the pizza for unsliced position.
2. Find largest valid slice using this position. Grow from current position in all directions. Allow overslapping with existing slices, as long as the remain valid after overlap.
3. If valid slice found - clear all the ingredients contained in the slice & add slice. Update all overlapped slices.
4. If scan not completed - go to 1 & continue the scan.

Solution score: 958,875 
- Example: 12
- Small: 35
- Medium: 49,523
- Big: 909,305

Please ignore code style. The objective was to complete the task as fast as possible.
