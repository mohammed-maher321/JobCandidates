﻿using JobCandidates.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Application.UserProfileUseCases.Queries
{
    public class GettAllUserProfilesListDto
    {
        public int RecordCount { get; set; }
        public List<UserProfile> UserProfileList { get; set; }

    }
}
