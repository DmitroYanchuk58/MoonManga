import { useState } from "react";
import PaginationBtn from "./pagination-btn";
import PageInput from "./pagination-input";
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
  const [activeInput, setActiveInput] = useState<"left" | "right" | null>(null);

  const handlePageSelect = (page: number) => {
    setActiveInput(null);
    onPageClick?.(page);
  };

  const handleInputConfirm = (page: number) => {
    setActiveInput(null);
    const targetPage = Math.max(1, Math.min(page, maxPageNumber));
    onPageClick?.(targetPage);
  };

  const renderPageButtons = () => {
    const buttons: React.ReactElement[] = [];
    const gap = 1;

    const wrap = (btn: React.ReactElement, page: number) => (
      <div
        key={btn.key}
        onMouseDown={(e) => {
          e.preventDefault();
          handlePageSelect(page);
        }}
      >
        {btn}
      </div>
    );

    if (maxPageNumber <= 5) {
      for (let i = 1; i <= maxPageNumber; i++) {
        buttons.push(
          wrap(
            <PaginationBtn key={i} label={i} isActive={i === currentPage} />,
            i,
          ),
        );
      }
      return buttons;
    }

    buttons.push(
      wrap(<PaginationBtn key={1} label={1} isActive={currentPage === 1} />, 1),
    );

    if (currentPage > 3) {
      buttons.push(
        activeInput === "left" ? (
          <PageInput key="input-left" onConfirm={handleInputConfirm} />
        ) : (
          <PaginationBtn
            key="dots-left"
            label="..."
            onClick={() => setActiveInput("left")}
          />
        ),
      );
    }

    let start = Math.max(2, currentPage - gap);
    let end = Math.min(maxPageNumber - 1, currentPage + gap);
    if (currentPage <= 3) end = 4;
    if (currentPage >= maxPageNumber - 2) start = maxPageNumber - 3;

    for (let i = start; i <= end; i++) {
      buttons.push(
        wrap(
          <PaginationBtn key={i} label={i} isActive={i === currentPage} />,
          i,
        ),
      );
    }

    if (currentPage < maxPageNumber - 2) {
      buttons.push(
        activeInput === "right" ? (
          <PageInput key="input-right" onConfirm={handleInputConfirm} />
        ) : (
          <PaginationBtn
            key="dots-right"
            label="..."
            onClick={() => setActiveInput("right")}
          />
        ),
      );
    }

    buttons.push(
      wrap(
        <PaginationBtn
          key={maxPageNumber}
          label={maxPageNumber}
          isActive={currentPage === maxPageNumber}
        />,
        maxPageNumber,
      ),
    );

    return buttons;
  };

  return (
    <div className="pagination-block">
      <div className="pagination">
        <div
          onMouseDown={(e) => {
            e.preventDefault();
            setActiveInput(null);
            onLeftArrowClick?.();
          }}
        >
          <PaginationBtn icon="/icons/arrow-left.svg" />
        </div>

        {renderPageButtons()}

        <div
          onMouseDown={(e) => {
            e.preventDefault();
            setActiveInput(null);
            onRightArrowClick?.();
          }}
        >
          <PaginationBtn icon="/icons/arrow-right.svg" />
        </div>
      </div>
    </div>
  );
};
