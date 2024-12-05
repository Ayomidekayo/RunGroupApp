using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RunGroupApp.Controllers;
using RunGroupApp.Interface;
using RunGroupApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroupApp.Testing.Controller
{
    public class ClubControllerTest
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly ClubController _clubController;
        public ClubControllerTest()
        {
            _clubRepository = A.Fake<IClubRepository>();
            _photoService = A.Fake<IPhotoService>();
            _clubController = A.Fake<ClubController>();
        }
        [Fact]
        public void ClubController_Index_ReturnSuccess()
        {
            //Arrenage
            var club = A.Fake<IEnumerable<Club>>();
            A.CallTo(() => _clubRepository.GetAllClubAsync()).Returns(club);
            //Act
            var result = _clubController.Index();
            //Asert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void ClubController_Details_ReturnSuccess()
        {
            //arrrenge
            var id = 1;
            var club=A.Fake<Club>();
            A.CallTo(() => _clubRepository.GetClubAsync(id)).Returns(club);
            //Act
            var result=_clubController.Details(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
            

        }
    }