// Copyright (c) Microsoft. All rights reserved.

using System.ComponentModel;
using Microsoft.SemanticKernel.SkillDefinition;

public class ReasonSkill
{
    private static readonly string[] s_reasons = {
        "I did not do my homework",
        "I am late for an appointment",
        "I forgot to call home"
    };

    [SKFunction, Description("Return a random reason for use the excuse prompt.")]
    public string RandomReason()
    {
        Random random = new Random();

        int index = random.Next(s_reasons.Length);

        return s_reasons[index];
    }
}
