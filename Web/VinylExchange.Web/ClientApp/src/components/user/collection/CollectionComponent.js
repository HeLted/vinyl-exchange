import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight, faTimes } from "@fortawesome/free-solid-svg-icons";
import PlayerLoaderButton from "./../../common/PlayerLoaderButton";
import GoToReleaseButton from "./../../common/GoToReleaseButton";
import AddSalePopupFormContainer from "./../../common/popupForms/addSalePopupForm/AddSalePopupContainer";
import "./Collection.css";
import AddSalePopupContainer from "./../../common/popupForms/addSalePopupForm/AddSalePopupContainer";

function CollectionCoponent(props) {
  return (
    <div className="col-3">
      <div className="card" >
        <img
          className="card-img-top"
          src={props.data.coverArt}
          alt="Card image cap"
        />
        <div className="card-body">
          <p className="card-title">
            {props.data.artist} - {props.data.title}
          </p>
          <div className="collection-item-info">
            <h6>
              Vinyl:
              <span className="grade-badge badge badge-primary">
                {props.data.vinylGrade}
              </span>
            </h6>
            <h6>
              Sleeve:
              <span className="grade-badge badge badge-primary">
                {props.data.sleeveGrade}
              </span>
            </h6>
            <p className="card-text">
              Description:
              {props.data.description == ""
                ? "No description"
                : props.data.description}
            </p>
          </div>
          <div className="row">
          <div className="col-3">
            <button
              type="button"
              className="btn-spr btn btn-danger"
              onClick={() =>
                props.functions.handleRemoveFromCollection(props.data.id)
              }
            >
              Remove From Collection <FontAwesomeIcon icon={faTimes} />
            </button>

            <PlayerLoaderButton data={{ releaseId: props.data.releaseId }} />
            <AddSalePopupContainer
              data={{
                collectionItemId: props.data.id,
                releaseId : props.data.releaseId,
                vinylGrade: props.data.vinylGrade,
                sleeveGrade: props.data.sleeveGrade,
                description: props.data.description
              }}
            />
            <GoToReleaseButton data={{ releaseId: props.data.releaseId }} />
          </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CollectionCoponent;
