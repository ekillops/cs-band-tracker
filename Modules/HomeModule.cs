using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System.Linq;
using BandTracker.Objects;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] =_=> View["index.cshtml"];

////BANDS////
      Get["/bands"] =_=> {
        List<Band> allBands = Band.GetAll();
        return View["all_bands.cshtml", allBands];
      };

      Get["/bands/new"] =_=> View["new_band_form.cshtml"];

      Post["/bands/new"] =_=> {
        string newBandName = Request.Form["band-name"];
        int newBandMembers = int.Parse(Request.Form["number-members"]);
        Band newBand = new Band(newBandName, newBandMembers);
        newBand.Save();
        return View["band.cshtml", newBand];
      };

      Get["/bands/{id}"] = parameters => {
        Band foundBand = Band.Find(parameters.id);
        return View["band.cshtml", foundBand];
      };

      Get["/bands/{id}/update"] = parameters => {
        Band foundBand = Band.Find(parameters.id);
        return View["band_update.cshtml", foundBand];
      };

      Patch["/bands/update"] = parameters => {
        string newBandName = Request.Form["band-name"];
        int newBandMembers = int.Parse(Request.Form["number-members"]);
        int targetId = int.Parse(Request.Form["target-id"]);
        Band.Update(targetId, newBandName, newBandMembers);
        Band foundBand = Band.Find(targetId);
        return View["band.cshtml", foundBand];
      };

      Delete["/bands/delete"] =_=> {
        int targetId = int.Parse(Request.Form["target-id"]);
        Band.Delete(targetId);
        List<Band> allBands = Band.GetAll();
        return View["all_bands.cshtml", allBands];
      };

      Delete["/bands/delete_all"] =_=> {
        Band.DeleteAll();
        List<Band> allBands = Band.GetAll();
        return View["all_bands.cshtml", allBands];
      };


////VENUES////
      Get["/venues"] =_=> {
        List<Venue> allVenues = Venue.GetAll();
        return View["all_venues.cshtml", allVenues];
      };

      Get["/venues/new"] =_=> View["new_venue_form.cshtml"];

      Post["/venues/new"] =_=> {
        string newVenueName = Request.Form["venue-name"];
        string newVenueSize = Request.Form["venue-size"];
        int newVenueCapacity = int.Parse(Request.Form["venue-capacity"]);
        Venue newVenue = new Venue(newVenueName, newVenueSize, newVenueCapacity);
        newVenue.Save();
        return View["venue.cshtml", newVenue];
      };

      Get["/venues/{id}"] = parameters => {
        Venue foundVenue = Venue.Find(parameters.id);
        return View["venue.cshtml", foundVenue];
      };

      Get["/venues/{id}/update"] = parameters => {
        Venue foundVenue = Venue.Find(parameters.id);
        return View["venue_update.cshtml", foundVenue];
      };

      Patch["/venues/update"] = parameters => {
        string newVenueName = Request.Form["venue-name"];
        string newVenueSize = Request.Form["venue-size"];
        int newVenueCapacity = int.Parse(Request.Form["venue-capacity"]);
        int targetId = int.Parse(Request.Form["target-id"]);
        Venue.Update(targetId, newVenueName, newVenueSize, newVenueCapacity);
        Venue foundVenue = Venue.Find(targetId);
        return View["venue_update.cshtml", foundVenue];
      };

      Delete["/venues/delete"] =_=> {
        int targetId = int.Parse(Request.Form["target-id"]);
        Venue.Delete(targetId);
        List<Venue> allVenues = Venue.GetAll();
        return View["all_venues.cshtml", allVenues];
      };

      Delete["/venues/delete_all"] =_=> {
        Venue.DeleteAll();
        List<Venue> allVenues = Venue.GetAll();
        return View["all_venues.cshtml", allVenues];
      };

////GENRES////
      Get["/genres"] =_=> {
        List<Genre> allGenres = Genre.GetAll();
        return View["all_genres.cshtml", allGenres];
      };

      Get["/genres/new"] =_=> View["new_genre_form.cshtml"];

      Post["/genres/new"] =_=> {
        string newGenreName = Request.Form["genre-name"];
        Genre newGenre = new Genre(newGenreName);
        newGenre.Save();
        return View["genre.cshtml", newGenre];
      };

      Get["/genres/{id}"] = parameters => {
        Genre foundGenre = Genre.Find(parameters.id);
        return View["genre.cshtml", foundGenre];
      };

      Delete["/genres/delete"] =_=> {
        int targetId = int.Parse(Request.Form["target-id"]);
        Genre.Delete(targetId);
        List<Genre> allGenres = Genre.GetAll();
        return View["all_genres.cshtml", allGenres];
      };

      Delete["/genres/delete_all"] =_=> {
        Genre.DeleteAll();
        List<Genre> allGenres = Genre.GetAll();
        return View["all_genres.cshtml", allGenres];
      };

      Get["/genres/{id}/update"] = parameters => {
        Genre foundGenre = Genre.Find(parameters.id);
        return View["genre_update.cshtml", foundGenre];
      };

      Patch["/genres/update"] = parameters => {
        string newGenreName = Request.Form["genre-name"];
        int targetId = int.Parse(Request.Form["target-id"]);
        Genre.Update(targetId, newGenreName);
        Genre foundGenre = Genre.Find(targetId);
        return View["genre_update.cshtml", foundGenre];
      };

      Get["/venue-genre"] =_=> {
        List<Venue> allVenues = Venue.GetAll();
        List<Genre> allGenres = Genre.GetAll();

        Dictionary<string, object> returnModel = new Dictionary<string, object>()
        {
          {"venues", allVenues},
          {"genres", allGenres}
        };

        return View["venue_genre.cshtml", returnModel];
      };

      Post["/venue-genre"] =_=> {
        int venueId = int.Parse(Request.Form["venue"]);
        int genreId = int.Parse(Request.Form["genre"]);

        Venue foundVenue = Venue.Find(venueId);
        foundVenue.AddGenre(genreId);
        return View["venue.cshtml", foundVenue];
      };

      Get["/band-genre"] =_=> {
        List<Band> allBands = Band.GetAll();
        List<Genre> allGenres = Genre.GetAll();

        Dictionary<string, object> returnModel = new Dictionary<string, object>()
        {
          {"bands", allBands},
          {"genres", allGenres}
        };

        return View["band_genre.cshtml", returnModel];
      };

      Post["/band-genre"] =_=> {
        int bandId = int.Parse(Request.Form["band"]);
        int genreId = int.Parse(Request.Form["genre"]);

        Band foundBand = Band.Find(bandId);
        foundBand.AddGenre(genreId);
        return View["band.cshtml", foundBand];
      };

// Performances
      Get["/performance"] =_=> {
        List<Band> allBands = Band.GetAll();
        List<Venue> allVenues = Venue.GetAll();

        Dictionary<string, object> returnModel = new Dictionary<string, object>()
        {
          {"bands", allBands},
          {"venues", allVenues}
        };
        return View["performance.cshtml", returnModel];
      };

      Post["/performance"] =_=> {
        int venueId = int.Parse(Request.Form["venue"]);
        int bandId = int.Parse(Request.Form["band"]);
        Band foundBand = Band.Find(bandId);
        foundBand.AddPerformance(venueId);
        return View["band.cshtml", foundBand];
      };
    }
  }
}
