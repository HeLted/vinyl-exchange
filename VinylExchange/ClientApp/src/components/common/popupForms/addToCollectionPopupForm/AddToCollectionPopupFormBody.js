import React from "react";

function AddToCollectionPopupFormBody(){
 return(
    <div
          class="modal fade"
          id="exampleModalCenter"
          tabindex="-1"
          role="dialog"
          aria-labelledby="exampleModalCenterTitle"
          aria-hidden="true"
        >
          <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLongTitle">
                  Add To Collection
                </h5>
                <button
                  type="button"
                  class="close"
                  data-dismiss="modal"
                  aria-label="Close"
                >
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <form>
                  <div class="form-group">
                    <label for="exampleFormControlSelect1">Item Grade</label>
                    <select class="form-control" id="exampleFormControlSelect1">
                      <option value="1">Mint </option>
                      <option value="2">Near Mint</option>
                      <option value="3">Very Good</option>
                      <option value="4">Good</option>
                      <option value="5">Fair</option>
                      <option value="6">Poor</option>
                    </select>
                  </div>
  
                  <div class="form-group">
                    <label for="exampleFormControlSelect1">Sleeve Grade</label>
                    <select class="form-control" id="exampleFormControlSelect1">
                      <option value="1">Mint </option>
                      <option value="2">Near Mint</option>
                      <option value="3">Very Good</option>
                      <option value="4">Good</option>
                      <option value="5">Fair</option>
                      <option value="6">Poor</option>
                    </select>
                  </div>
                  <div class="form-group">
                    <label for="exampleFormControlTextarea1">
                      Description
                    </label>
                    <textarea
                      class="form-control"
                      id="exampleFormControlTextarea1"
                      rows="3"
                    ></textarea>
                  </div>
                </form>
              </div>
              <div class="modal-footer">
                <button
                  type="button"
                  class="btn btn-secondary"
                  data-dismiss="modal"
                >
                  Close
                </button>
                <button type="button" class="btn btn-outline-primary">
                  Add to Collection
                </button>
              </div>
            </div>
          </div>
        </div>)
}

export default AddToCollectionPopupFormBody;