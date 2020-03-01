import React, { Component } from "react";
import $script from "scriptjs";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync } from "@fortawesome/free-solid-svg-icons";

class ChangeAvatarModalComponent extends Component {
  componentDidMount() {
    $script.get("/js/customFileUploadInput.js", function() {});
  }

  render() {
    return (
      <div className="modal" id="changeAvatarModal" tabIndex="-1" role="dialog">
        <div className="modal-dialog" role="document">
          <div className="modal-content text-center">
            <div className="modal-header">
              <h5 className="property-text-nm modal-title">Change Avatar</h5>
            </div>
            <div className="address-modal-body modal-body">
              <div className="row justify-content-center">
                <div className="container py-3">
                  <div className="input-group">
                    <div className="custom-file">
                      <input
                        type="file"
                        className="file-input custom-file-input"
                        id="avatarInput"
                        aria-describedby="myInput"
                        onChange={this.props.functions.handleOnFileUpload}
                      />
                      <label className="custom-file-label" htmlFor="myInput">
                        Choose file
                      </label>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div className="modal-footer">
              <button
                type="button"
                className="btn btn-secondary"
                data-dismiss="modal"
              >
                Close
              </button>
              {this.props.data.isLoading ? ( <button
                type="button"
                className="btn btn-success"
              >
               <FontAwesomeIcon icon={faSync} spin />
              </button>) : ( <button
                type="button"
                className="btn btn-success"
                onClick={this.props.functions.handleOnSubmit}
              >
                Submit
              </button>)}
             
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default ChangeAvatarModalComponent;
