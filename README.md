# An Application That Backs Up Files To Cloud Storage Using [Akka.NET]

This application backs up zipped folders from a PC to a cloud storage location such as OneDrive. 
I created a version of this using conventional .NET code and then, as a learning exercise, I re-implemented it using [Akka.NET]. 

I identify two types of folders - __Regular__ folders and __Visual Studio Project__ folders. 

For the former I zip and then encrypt them before backing up (encryption technique omitted).

For the latter I create a zip with the bin and obj folders omitted and then back them up.

The input is from a text file, SourceFolderPaths.txt, that contains a list of fully qualified paths to folders that require backing up.

### Tech

* [Akka.NET] - A toolkit and runtime for building highly concurrent, distributed, and fault tolerant event-driven applications on .NET & Mono, based on the [Actor Model] of concurrency.
* [NLog] - A free logging platform for .NET, Silverlight and Windows Phone with rich log routing and management capabilities.
* [ZipDiff] - A utility for comparing the contents of 2 different zip files.


### Overview

##### Actors

* __CloudBackupActor__ - Manages cloud backup from start to finish. Creates the other actors. Receives StartMessage, IncrementFolderCountMessage. Sends ZipMessage.
* __ZipActor__ - Manages zip operations on folders. Receives ZipMessage. Sends BackupMessage, IncrementFolderCountMessage, FolderChangesMessage and "no changes" message. 
* __BackupActor__ - Backs up zip archive to cloud storage. Receives BackupMessage. Sends IncrementFolderCountMessage.
* __BackupStatisticsActor__ - Logs statistics showing changes to folders. Receives FolderChangesMessage and "no changes" message.
  
##### Messages

* __StartMessage__ - Instruction to start actor processing.
* __StartedMessage__ - Indicates actor processing has started.
* __ZipMessage__ - Instruction to perform a zip operation.
* __IncrementFolderCountMessage__ - Instruction to increment the folder count after processing a folder with a specific kind of zip operation.
* __FolderCountIncrementedMessage__ - Indicates folder count has incremented, i.e., a folder has been processed.
* __FolderChangesMessage__ - Represents changes to files in a folder.
* __BackupMessage__ - Instruction for backing up a zip archive to cloud storage.

##### Features

* Actor routing using a _RoundRobinPool_ strategy.

* Actor supervision using a _OneForOne_ strategy.

* Predicate message filters (receiving a message from an actor and handling it differently based on a condition).

* Actor logging.

See also [What is an Actor?] and [Akka.NET cheat sheet].

[Actor Model]: <https://en.wikipedia.org/wiki/Actor_model>
[Akka.NET]: <http://getakka.net/>
[NLog]: <http://nlog-project.org/>
[ZipDiff]: <https://github.com/leekelleher/ZipDiff/>
[What is an Actor?]: <https://petabridge.com/blog/akkadotnet-what-is-an-actor/>
[Akka.NET cheat sheet]: <http://dontcodetired.com/live/downloads/akkacheat/AkkaDotNetCheatSheet.pdf>