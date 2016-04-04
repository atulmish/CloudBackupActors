﻿using System;
using System.Diagnostics;
using Akka.Actor;
using CloudBackupActors.Actors;
using CloudBackupActors.Messages;
using NLog;

namespace CloudBackupActors
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string starting = LogMessageParts.Starting;
            Console.WriteLine(starting);
            Logger.Info(starting);
            Console.WriteLine(Environment.NewLine);

            using (var actorSystem = ActorSystem.Create("CloudBackupActorSystem"))
            {
                var actor = actorSystem.ActorOf(Props.Create<CloudBackupActor>(), "CloudBackup");

                actor.Tell(new StartMessage());

                actorSystem.AwaitTermination();
                Console.WriteLine("Actor system shutdown...");
            }

            stopwatch.Stop();
            string finished = string.Format(LogMessageParts.FinishedIn, (float)stopwatch.ElapsedMilliseconds / 1000);
            Console.WriteLine(finished);
            Logger.Info(finished);
        }
    }
}