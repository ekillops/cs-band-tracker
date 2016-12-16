using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
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
        return View["band.cshtml", foundBand];
      };

      Patch["/band/{id}/update"] = parameters => {
        string newBandName = Request.Form["band-name"];
        Band.Update(parameters.id, newBandName);
        Band foundBand = Band.Find(parameters.id);
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
        return View["update_venue.cshtml", foundVenue];
      };

      Patch["/venues/{id}/update"] = parameters => {
        string newVenueName = Request.Form["venue-name"];
        string newVenueSize = Request.Form["venue-size"];
        int newVenueCapacity = int.Parse(Request.Form["venue-capacity"]);
        Venue.Update(parameters.id, newVenueName, newVenueSize, newVenueCapacity);
        Venue foundVenue = Venue.Find(parameters.id);
        return View["update_venue.cshtml", foundVenue];
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
        return View["update_genre.cshtml", foundGenre];
      };

      Patch["/genre/{id}/update"] = parameters => {
        string newGenreName = Request.Form["genre-name"];
        Genre.Update(parameters.id, newGenreName);
        Genre foundGenre = Genre.Find(parameters.id);
        return View["update_genre.cshtml", foundGenre];
      };
    }
  }
}
