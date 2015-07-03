using FacebookPhotoUploader.API.Interfaces;
using FacebookPhotoUploader.API.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FacebookPhotoUploader.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IFacebookService facebookService;
        private ObservableCollection<Album> _albums;

        public MainViewModel(IFacebookService facebookService)
        {
            this.facebookService = facebookService;
            this._albums = new ObservableCollection<Album>();
            if (IsInDesignMode)
            {
                _albums.Add(new Album()
                {
                    Id = "4323424",
                    Name = "Album 1",
                    Description = "Album 1 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "https://scontent.xx.fbcdn.net/hphotos-xpa1/v/t1.0-9/p75x225/1471789_708303299189111_54080471_n.jpg?oh=783ea12a742de6405af54b46b77201ae&oe=56125BA2"
                });

                _albums.Add(new Album()
                {
                    Id = "4323321424",
                    Name = "Album 2",
                    Description = "Album 2 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "Assets/LightGray.png"
                });
                _albums.Add(new Album()
                {
                    Id = "4323424",
                    Name = "Album 1",
                    Description = "Album 1 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "Assets/LightGray.png"
                });

                _albums.Add(new Album()
                {
                    Id = "4323321424",
                    Name = "Album 2",
                    Description = "Album 2 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "https://scontent.xx.fbcdn.net/hphotos-xpa1/v/t1.0-9/p75x225/1471789_708303299189111_54080471_n.jpg?oh=783ea12a742de6405af54b46b77201ae&oe=56125BA2"
                }); _albums.Add(new Album()
                {
                    Id = "4323424",
                    Name = "Album 1",
                    Description = "Album 1 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "https://scontent.xx.fbcdn.net/hphotos-xpa1/v/t1.0-9/p75x225/1471789_708303299189111_54080471_n.jpg?oh=783ea12a742de6405af54b46b77201ae&oe=56125BA2"
                });

                _albums.Add(new Album()
                {
                    Id = "4323321424",
                    Name = "Album 2",
                    Description = "Album 2 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "https://scontent.xx.fbcdn.net/hphotos-xpa1/v/t1.0-9/p75x225/1471789_708303299189111_54080471_n.jpg?oh=783ea12a742de6405af54b46b77201ae&oe=56125BA2"
                }); _albums.Add(new Album()
                {
                    Id = "4323424",
                    Name = "Album 1",
                    Description = "Album 1 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "https://scontent.xx.fbcdn.net/hphotos-xpa1/v/t1.0-9/p75x225/1471789_708303299189111_54080471_n.jpg?oh=783ea12a742de6405af54b46b77201ae&oe=56125BA2"
                });

                _albums.Add(new Album()
                {
                    Id = "4323321424",
                    Name = "Album 2",
                    Description = "Album 2 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "https://scontent.xx.fbcdn.net/hphotos-xpa1/v/t1.0-9/p75x225/1471789_708303299189111_54080471_n.jpg?oh=783ea12a742de6405af54b46b77201ae&oe=56125BA2"
                }); _albums.Add(new Album()
                {
                    Id = "4323424",
                    Name = "Album 1",
                    Description = "Album 1 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "https://scontent.xx.fbcdn.net/hphotos-xpa1/v/t1.0-9/p75x225/1471789_708303299189111_54080471_n.jpg?oh=783ea12a742de6405af54b46b77201ae&oe=56125BA2"
                });

                _albums.Add(new Album()
                {
                    Id = "4323321424",
                    Name = "Album 2",
                    Description = "Album 2 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "Assets/LightGray.png"
                }); _albums.Add(new Album()
                {
                    Id = "4323424",
                    Name = "Album 1",
                    Description = "Album 1 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "https://scontent.xx.fbcdn.net/hphotos-xpa1/v/t1.0-9/p75x225/1471789_708303299189111_54080471_n.jpg?oh=783ea12a742de6405af54b46b77201ae&oe=56125BA2"
                });

                _albums.Add(new Album()
                {
                    Id = "4323321424",
                    Name = "Album 2",
                    Description = "Album 2 Description",
                    Created = DateTime.Now.AddDays(-30),
                    Updated = DateTime.Now,
                    CoverPhotoLink = "https://scontent.xx.fbcdn.net/hphotos-xpa1/v/t1.0-9/p75x225/1471789_708303299189111_54080471_n.jpg?oh=783ea12a742de6405af54b46b77201ae&oe=56125BA2"
                });
            }
        }

        public async Task GetAlbumsAsync()
        {
            if (await facebookService.IsLoggedInAsync())
            {
                var albums = await facebookService.GetAlbumsAsync();
                foreach (var album in albums)
                {
                    AddAlbum(album);
                }
            }
            else
            {
                await facebookService.LoginAsync();
            }
        }

        public async Task LoginAsync()
        {
            await facebookService.LoginAsync();
        }

        public IList<Album> Albums
        {
            get { return _albums; }
        }

        private void AddAlbum(Album album)
        {
            if (this._albums == null)
            {
                _albums = new ObservableCollection<Album>();
            }

            var exists = _albums.Where(a => a.Id == album.Id).SingleOrDefault();
            if (exists == null)
            {
                _albums.Add(album);
            }
        }
    }
}