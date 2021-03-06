﻿using Microsoft.Build.Framework;
using System;
using System.Threading;

// MagicOnion.MSBuild.Tasks.MagicOnionGenerator
namespace MagicOnion.MSBuild.Tasks
{
    public class MagicOnionGenerator : Microsoft.Build.Utilities.Task
    {
        [Required]
        public string Input { get; set; }

        [Required]
        public string Output { get; set; }

        public string ConditionalSymbol { get; set; }

        public string Namespace { get; set; }

        public bool? UnuseUnityAttr { get; set; }

        public override bool Execute()
        {
            try
            {
                new MagicOnionCompiler(x => this.Log.LogMessage(x), CancellationToken.None)
                    .GenerateFileAsync(
                        Input,
                        Output,
                        UnuseUnityAttr ?? false,
                        Namespace ?? "MagicOnion",
                        ConditionalSymbol
                    )
                    .GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.Log.LogErrorFromException(ex, true);
                return false;
            }

            return true;
        }
    }
}
