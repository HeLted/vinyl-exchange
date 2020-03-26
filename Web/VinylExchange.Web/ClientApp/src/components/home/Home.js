import React, { Component } from "react";
import $script from "scriptjs";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";

export class Home extends Component {
  static displayName = Home.name;

  componentDidMount() {
    $script.get("/js/homePage.js", function() {});
  }

  render() {
    return (
      <section id="home" >
        <div className="overlay"></div>
        <div className="container">
          <div className="row">
            <div className="col-md-8 col-sm-12">
              <div className="home-text">
                <h2 style={{color:"white"}}>Be Part Of The Vinyl Renesance!</h2>
                <p>
                  Visit our marketplace and have fun!
                </p>
                <ul className="section-btn">
                  <Link className="btn btn-success btn-lg" to="/Marketplace">
                    Go To Marketplace <FontAwesomeIcon icon={faArrowRight} />
                  </Link>
                </ul>
              </div>
            </div>
          </div>
        </div>

        <video controls autoPlay loop muted>
          <source src="/videos/homePageVideo.mp4" type="video/mp4" />
          Your browser does not support the video tag.
        </video>
      </section>
    );
  }
}
