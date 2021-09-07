using BlazorMovies.Shared.Entities;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BlazorMovies.Client.Pages
{
    public partial class Counter
    {
        [Inject] SingletonService singleton { get; set; }
        [Inject] TransientService transient { get; set; }
[Inject] IJSRuntime js { get; set; }
        private List<Movie> movies;
        protected override void OnInitialized()
        {
            movies = new List<Movie>()
            {
                new Movie(){Title = "Joker", ReleaseDate = new DateTime(2019,3,28)},
                new Movie(){Title = "Avengers", ReleaseDate = new DateTime(2019,3,28)},
            };
        }



        private int currentCount = 0;
        private static int currentCountStatic = 0;

        [JSInvokable]
        public async Task IncrementCount()
        {
            currentCount++;
            singleton.Value += 1;
            transient.Value += 1;
            currentCountStatic++;
            await js.InvokeVoidAsync("dotnetStaticInvocation");
        }

        private async Task IncrementCountJavaScript()
        {
            await js.InvokeVoidAsync("dotnetInstanceInvocation", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public static Task<int> GetCurrentCount()
        {
            return Task.FromResult(currentCountStatic);
        }
    }
}
