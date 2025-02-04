﻿using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataEntities
{
    public class Repo
    {
        public string Name { get; set; }
        public List<RepositoryLanguage> Languages { get; set; }
        public int Stars { get; set; }
        public DateTimeOffset LastCommit { get; set; }
        public int PullRequests { get; set; }
        public string SiteUrl { get; set; }

    }
}
