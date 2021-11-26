import React, { useEffect, useState } from "react";
import { getItemById } from "../../actions/actionsItems";
import { connect } from "react-redux";
import { compose } from "redux";
import BuyItemModal from "./BuyItemModal";

function ReviewItem(props) {
  const [item, setItem] = useState({});
  const [showPostModal, setShowPostModal] = useState(false);

  useEffect(() => {
    debugger;
    props.getItemById(localStorage.getItem("item-productkey"));
  }, []);

  const Item = () => {
    debugger;
    if (props.loadedImage === undefined) {
      return null;
    }
    debugger;
    console.log(sessionStorage.getItem("userIdAgentApp"));
    return (
      <img
        src={"data:image/jpg;base64," + props.loadedImage.fileContents}
        style={{ width: 600, height: 320 }}
        className="mb-3"
      />
    );
  };

  const displayModalPost = (item) => {
    if (item != undefined) {
      setItem(item);
    }
    setShowPostModal(!showPostModal);
  };

  if (props.item === undefined) {
    return null;
  }

  return (
    <React.Fragment>
      {showPostModal ? (
        <BuyItemModal
          show={showPostModal}
          item={item}
          onShowChange={() => displayModalPost()}
        />
      ) : null}
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <Item />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label className="label">Name:</label>
            <input
              type="text"
              name="name"
              class="form-control"
              id="name"
              value={props.item.name}
              disabled={true}
              placeholder="Enter name of product"
            />
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Price:</label>
            <input
              type="number"
              name="price"
              class="form-control"
              id="price"
              value={props.item.price}
              disabled={true}
              placeholder="Enter price"
            />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">Description:</label>
            <textarea
              name="promotedAction"
              value={props.item.description}
              cols="40"
              rows="5"
              class="form-control"
              disabled={true}
              placeholder="Enter promotion/action"
            ></textarea>
          </div>
        </div>
      </div>
      {/* {sessionStorage.getItem("userIdAgentApp") === undefined ||
      sessionStorage.getItem("userIdAgentApp") === "" ? (
        ""
      ) : sessionStorage.getItem("roleAgentApp") === "Agent" ? (
        <div style={{ textAlign: "center" }} className="mt-5 pb-5">
          <button
            onClick={() => {
              editItem();
            }}
            className="btn btn-danger"
          >
            Edit
          </button>
          <span style={{ width: 25, display: "inline-block" }}></span>
          <button
            onClick={() => {
              deleteItem();
            }}
            className="btn btn-danger"
          >
            Delete
          </button>
        </div>
      ) : (
        <div style={{ textAlign: "center" }} className="mt-5 pb-5">
          <button
            onClick={() => displayModalPost(props.item)}
            className="btn btn-primary"
          >
            Buy
          </button>
        </div>
      )} */}
      <div style={{ textAlign: "center" }} className="mt-5 pb-5">
        <button
          onClick={() => displayModalPost(props.item)}
          className="btn btn-primary"
        >
          Add to cart
        </button>
      </div>
    </React.Fragment>
  );
}

const mapStateToProps = (state) => ({
  item: state.item,
  loadedImage: state.loadedImage,
});

export default compose(
  connect(mapStateToProps, {
    getItemById,
  })
)(ReviewItem);
