# A Proof of Concept to see how System.CommandLine Commands could be composed from plugins, into a host application.

Supporting extensibility for command line tools is an important way to allow a community to add features that may not be a priority for the core maintainers.

A first hurdle for supporting this scenario is the ability to add new commands into application to light up new features.

This is a simple PoC to see if this could work. 

The original spike used a vanilla AssemblyLoadContext but this showed some issues resolving System.CommandLine types, so this second attempt used the [.NET Core Plugins](https://github.com/natemcmaster/DotNetCorePlugins)