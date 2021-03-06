using System.Collections.Generic;
using Xunit;

namespace RadioSongsScraper.Unit.Tests
{
    public class RadioSongsScraperUnitTests
    {
        [Fact]
        public void ValidateUserChoice_EnterCorrectNumber_ReturnsTrue()
        {
            bool validation;

            validation = Program.ValidateUserChoice(5);

            Assert.True(validation);
        }

        [Fact]
        public void ValidateUserChoice_EnterNegativeNumber_ReturnsFalse()
        {
            bool validation;

            validation = Program.ValidateUserChoice(-5);

            Assert.False(validation);
        }

        [Fact]
        public void ValidateUserChoice_EnterNumberLargerThan15_ReturnsFalse()
        {
            bool validation;

            validation = Program.ValidateUserChoice(20);

            Assert.False(validation);
        }

        [Fact]
        public void GetSearchText_EnterUserChoice_ReturnsCorrectText()
        {
            List<string> songs = new();
            int userChoice = 1;
            string searchText;

            songs.Add("SongName");
            searchText = Program.GetSearchText(songs, userChoice);

            Assert.Equal("SongName", searchText);
        }
    }
}
