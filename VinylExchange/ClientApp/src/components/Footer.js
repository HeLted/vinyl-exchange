import React, { Component } from "react";

class Footer extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }
  render() {
    return (
      <footer>
        <div className="footer" id="footer">
          <div className="container">
              
            <div className="row justify-content-center">
              <div className="col-lg-6 col-md-6 col-sm-6 col-xs-6 text-center align-self-center">
              <img src="img/appLogo.png" heigth="500px" width="350px" />
              </div>
              <div className="col-lg-3 col-sm-2 col-xs-3">
                <h3> </h3>
                <ul>
                 
                  <br />
                 
                </ul>
              </div>
              <div className="col-lg-3 col-sm-2 col-xs-3">
                <ul>
                  <li>
                    <h5>
                      {" "}
                      <a href="#" style={{marginTop: "5em"}}>
                        {" "}
                        ABOUT US
                      </a>{" "}
                    </h5>
                    <hr/>
                  </li>
                  <li>
                    {" "}
                    <h5>
                      <a href="#"> CURRENT SERIES </a>{" "}
                    </h5>
                                <hr/>
                  </li>
                  <li>
                    {" "}
                    <h5>
                      <a href="#"> THE HOUSE </a>{" "}
                    </h5>
                    <hr/>
                  </li>
                  <li>
                    {" "}
                    <h5>
                      <a href="#"> LOOKING BACK </a>{" "}
                    </h5>
                                <hr/>
                  </li>
                </ul>
              </div>
            </div>
          </div>

          <div className="footer-bottom">
            <div className="container">
              <p className="pull-left copyright">
                {" "}
                Copyright © VinylExchange 2020. All right reserved.{" "}
              </p>
            </div>
            <br/>
           
          </div>
          <br/>
        </div>
      </footer>
    );
  }
}

export default Footer;
