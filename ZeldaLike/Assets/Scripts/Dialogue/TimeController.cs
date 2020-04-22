﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Management
{
    public class TimeController : MonoBehaviour
    {
        public List<PlayableDirector> playableDirectors;
        public List<TimelineAsset> timelines;

        public void Play()
        {
            foreach (PlayableDirector playableDirector in playableDirectors)
            {
                playableDirector.Play();
            }
        }

        public void PlayFromTimelines(int index)
        {
            TimelineAsset selectedAsset;

            if (timelines.Count <= index)
            {
                selectedAsset = timelines[timelines.Count - 1];
            }
            else
            {
                selectedAsset = timelines[index];
            }

            playableDirectors[0].Play(selectedAsset);
        }

    }
}