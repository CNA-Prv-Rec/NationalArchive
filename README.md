# NationalArchive

Hello. This is in Net5. It should just run. 
If you run it without the unit tests in visual studio it is already set with a start argument that will work, otherwise the unit tests are to test the backend logic.
You can change the startup arguments in the project properties pages if you need to.

I looked through the data to try to find some more things that would fail in my unit tests but the title on the search view of the test data is never null 
whereas the title on the detail is. So I had a bit of trouble creating test cases due to not being able to find any data that would go all the way down the chain. Sorry about that. 
The code should still handle all your required scenarios, just I don't have a specific test for the lower down conditions.

