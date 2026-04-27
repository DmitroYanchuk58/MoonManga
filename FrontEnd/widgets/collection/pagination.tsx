import PaginationBtn from "./pagination-btn";
import "./pagination.css";

interface PaginationProps {
  maxPageNumber: number;
  currentPage: number;
  onRightArrowClick?: () => void;
  onLeftArrowClick?: () => void;
  onPageClick?: (page: number) => void;
}

export const Pagination = ({
  maxPageNumber,
  currentPage,
  onRightArrowClick,
  onLeftArrowClick,
  onPageClick,
}: PaginationProps) => {
  const renderPageButtons = () => {
    const buttons = [];
    const gap = 1;

    if (maxPageNumber <= 5) {
      for (let i = 1; i <= maxPageNumber; i++) {
        buttons.push(
          <PaginationBtn
            key={i}
            label={i}
            isActive={i === currentPage}
            onClick={() => onPageClick?.(i)}
          />,
        );
      }
      return buttons;
    }

    buttons.push(
      <PaginationBtn
        key={1}
        label={1}
        isActive={currentPage === 1}
        onClick={() => onPageClick?.(1)}
      />,
    );

    if (currentPage > 3) {
      buttons.push(<PaginationBtn key="dots-left" label="..." />);
    }

    let start = Math.max(2, currentPage - gap);
    let end = Math.min(maxPageNumber - 1, currentPage + gap);

    if (currentPage <= 3) end = 4;
    if (currentPage >= maxPageNumber - 2) start = maxPageNumber - 3;

    for (let i = start; i <= end; i++) {
      buttons.push(
        <PaginationBtn
          key={i}
          label={i}
          isActive={i === currentPage}
          onClick={() => onPageClick?.(i)}
        />,
      );
    }

    if (currentPage < maxPageNumber - 2) {
      buttons.push(<PaginationBtn key="dots-right" label="..." />);
    }

    buttons.push(
      <PaginationBtn
        key={maxPageNumber}
        label={maxPageNumber}
        isActive={currentPage === maxPageNumber}
        onClick={() => onPageClick?.(maxPageNumber)}
      />,
    );

    return buttons;
  };

  return (
    <div className="pagination-block">
      <div className="pagination">
        <PaginationBtn
          icon="/icons/arrow-left.svg"
          onClick={onLeftArrowClick}
        />

        {renderPageButtons()}

        <PaginationBtn
          icon="/icons/arrow-right.svg"
          onClick={onRightArrowClick}
        />
      </div>
    </div>
  );
};
