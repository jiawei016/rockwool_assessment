import Modal from 'react-bootstrap/Modal';
import Image from 'react-bootstrap/Image';
import styles from './index.module.css';

const news_detail = (detailObj) => {
    return (
        <div>
            <Image src={detailObj.image_url} className={styles.news_detail_image} />
            <p className={styles.news_detail_title}>{detailObj.title}</p>
            <span className="badge bg-primary">{detailObj.country[0]}</span>&nbsp;
            <span className="badge bg-warning">{detailObj.category[0]}</span>&nbsp;
            <a href={detailObj.link} target="_blank" rel="noreferrer" className='badge bg-danger'>Click to See More</a>
            <p>{detailObj.description}</p>
        </div>
    )
}

export const NewsDetail = (props) => {
    let obj = props.detail;
    console.log(obj);
    return (
        <Modal show={props.show} onHide={props.handle_close}>
        <Modal.Header closeButton>
          <Modal.Title>
            Modal heading
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
            {
                obj != null ? news_detail(obj) : <></>
            }
        </Modal.Body>
        <Modal.Footer>
            {
                obj != null ? <span>Publish At - {obj.pubDate}</span> : <></>
            }
        </Modal.Footer>
      </Modal>
    )
}