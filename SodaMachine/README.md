# SodaMachine
Rewrite of the SodaMachine application to make it easier to maintain.

This is done by splitting up the various elements (classes, logic)
into their own files and making the various parts injectable.

I've also created a state object to keep track of the condition
of the soda machine, and keeping price as a fixed property of the
sodas themselves rather than hardcoding it (multiple places) in the
program logic.

Other than that I have opted to introduce as few changes to the user
interface as possible. This includes keeping the while-loop for the menu
rather than e.g. making the application event driven.

### Considerations
I've kept the application relatively low on dependencies by using
Microsoft's own libraries.

For personal preference I would use a few different packages when
designing from scratch.

* Autofac for dependency injection
* NUnit for unit testing (alternatively XUnit and FluentAssertions)
