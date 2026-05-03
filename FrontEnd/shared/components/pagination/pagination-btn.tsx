import "./pagination-btn.css";

interface PaginationBtnProps {
  label?: string | number;
  icon?: string;
  isActive?: boolean;
  onClick?: () => void;
}

const PaginationBtn = ({
  label,
  icon,
  isActive,
  onClick,
}: PaginationBtnProps) => {
  return (
    <button
      className={`pagination-btn ${isActive ? "active" : ""}`}
      onClick={onClick}
    >
      {icon ? <img src={icon} alt="pagination icon" /> : label}
    </button>
  );
};

export default PaginationBtn;
