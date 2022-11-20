## PR Checklist
Please check if your PR fulfills the following requirements:

- [ ] Compile without errors
- [ ] Run from the command line
- [ ] Pass all automated test cases
- [ ] Demonstrate your knowledge of automated testing by implementing both unit and acceptance tests
- [ ] Preserve existing requirements:
  - [ ] You must enter a comma delimited list of Dish Types with at least one selection
  - [ ] The output must print Dish Names in the following order: entrée, side, drink, dessert
  - [ ] If invalid selection is encountered, then print "error"
  - [ ] Ignore whitespace in the input
  - [ ] Each Dish Type is optional (i.e. can have zero if not entered in the input)
  - [ ] You can have multiple orders of potatoes (but still no more than 1 each of the other Dish Types)
  - [ ] If more than one Dish Type is entered, output it once, followed by "(xN)", e.g. "potato(x2)"
