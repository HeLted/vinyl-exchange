import React from "react";

function FailedAuthorizationComponent() {
  return (
    <div className="container-fluid">
      <div className="row border justify-content-center text-center">
        <div
          className="col-12 border-danger "
          style={{ width: "2000px", height: "500px", padding: "50px" }}
        >
          <h1 className="property-text" style={{ color: "red" }}>
            You Are Not Authorized To See This Page!
          </h1>
        </div>
      </div>
    </div>
  );
}

export default FailedAuthorizationComponent;
