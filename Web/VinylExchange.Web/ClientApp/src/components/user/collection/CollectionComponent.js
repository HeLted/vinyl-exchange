import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight, faTimes } from "@fortawesome/free-solid-svg-icons";
import PlayerLoaderButton from "./../../common/PlayerLoaderButton";
import GoToReleaseButton from "./../../common/GoToReleaseButton";
import "./Collection.css";
import AddSalePopupContainer from "./../../common/popupForms/addSalePopupForm/AddSalePopupContainer";
import GradeBadge from "./../../common/badges/GradeBadge";

function CollectionCoponent(props) {
  return (
    <div className="col-lg-4 col-md-6 col-sm-8 col-xs-12">
      <div
        className="row border text-center justify-content-center"
        style={{ padding: "30px", height: "600px" }}
      >
        <div className="col-12 ">
          <img
            class="img-thumbnail"
            src={props.data.coverArt}
            height="150px"
            width="150px"
          />
        </div>

        <div className="col-12">
          <div className="col-12 border ">
            <h6>Vinyl</h6>
          </div>
          <br/>
          <div className="col-12 ">
            <GradeBadge data={{ grade: props.data.vinylGrade }} />
          </div>
          <br/>
          <div className="col-12 border ">
            <h6>Sleeve</h6>
          </div>
          <br/>
          <div className="col-12 ">
            <GradeBadge data={{ grade: props.data.sleeveGrade }} />
          </div>
          <br/>
        </div>

        <div className="col-12">
          <br />
          <div className="row text-center justify-content-center border-left border-right border-top border-bottom">
            <div className="col-6 border-right p-0">
              <div
                class="btn-group"
                role="group"
                aria-label="Basic example"
              ></div>
              <button
                type="button"
                className="btn-spr btn btn-danger"
                onClick={() =>
                  props.functions.handleRemoveFromCollection(props.data.id)
                }
              >
                <FontAwesomeIcon icon={faTimes} />
              </button>
            </div>
            <div className="col-6 p-0">
              <PlayerLoaderButton data={{ releaseId: props.data.releaseId }} />
            </div>
          </div>

          <div className="row text-center justify-content-center border-left border-right border-bottom">
            <div className="col-6 border-right p-0">
              <AddSalePopupContainer
                data={{
                  collectionItemId: props.data.id,
                  releaseId: props.data.releaseId,
                  vinylGrade: props.data.vinylGrade,
                  sleeveGrade: props.data.sleeveGrade,
                  description: props.data.description
                }}
              />
            </div>
            <div className="col-6  p-0">
              <GoToReleaseButton data={{ releaseId: props.data.releaseId }} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CollectionCoponent;
