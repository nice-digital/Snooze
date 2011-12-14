﻿using System.Linq;
using System.Web.Mvc;
using Fizzler.Systems.HtmlAgilityPack;
using Machine.Specifications;
using SampleApplication.Controllers;
using Snooze.MSpec;
using Snooze.Routing;

namespace Snooze
{
	public class RoutableHandler : Handler
	{
		static Register route = r => r.Map<Command>(u => "commandhandler/" + u.stupid);

		public class StupidType
		{
			public string Value { get; set; }
		}

		public class Command : Url
		{
			public StupidType stupid { get; set; }
		}

		public ResourceResult Get(Command cmd) { return OK(cmd); }
	}

	public class handlers_are_routable : with_controller<RoutableHandler.Command, RoutableHandler>
	{
		Establish context = () =>
		{
		};

		Because of = () => get("commandhandler/stupid");

		It is_routable = is_200;

	    It method_is_set = () => class_under_test.HttpVerb.ShouldEqual(HttpVerbs.Get);
	}

    
    public class access_viewbag : with_controller<HomeViewModel, HomeController>
    {
        Establish view_location = () =>
        {
            application_under_test_is_here("../SampleApplication");
            class_under_test.ViewBag.Stuff = "stuff";
        };

        Because of = () => get("");

        It content_negotiates_texthtml = () => conneg_html()
                .DocumentNode.InnerText.ShouldContain("HELLO");

        private It has_viewbag_content =
            () => conneg_html().DocumentNode.QuerySelectorAll("html").First().InnerText.ShouldContain("stuff");

        It has_no_parse_errors = () => markup_is_valid_according_to_dtd();
    }

	public class content_negotiate_texthtml : with_controller<HomeViewModel,HomeController>
	{
		Establish view_location = () => application_under_test_is_here("../SampleApplication");

		Because of = () => get("");

		It content_negotiates_texthtml = () => conneg_html()
				.DocumentNode.InnerText.ShouldContain("HELLO");

		It has_html_element = () => conneg_html().DocumentNode.QuerySelectorAll("html").ShouldNotBeEmpty();

		It has_no_parse_errors = () => markup_is_valid_according_to_dtd();
	}


	public class content_negotiate_json : with_controller<HomeViewModel, HomeController>
	{
		Establish view_location = () => application_under_test_is_here("../SampleApplication");

		Because of = () => get("");

		It content_negotiates_json = () => conneg_json().ShouldNotBeEmpty();
	}
}