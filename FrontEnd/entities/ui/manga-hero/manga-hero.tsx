import "./manga-hero.css";

interface MangaHeroProps {
  title: string;
  coverUrl: string;
  bannerUrl: string;
  volume: string;
}

export const MangaHero = ({
  title,
  coverUrl,
  bannerUrl,
  volume,
}: MangaHeroProps) => {
  return (
    <div className="manga-hero">
      <div className="manga-hero__banner-wrapper">
        <img src={bannerUrl} className="manga-hero__banner-img" alt="" />
        <div className="manga-hero__overlay" />
      </div>

      <div className="manga-hero__content">
        <div className="manga-hero__cover-wrapper">
          <img src={coverUrl} className="manga-hero__cover-img" alt={title} />
          <div className="manga-hero__volume-tag">{volume}</div>
        </div>
        <h1 className="manga-hero__title">{title}</h1>
      </div>
    </div>
  );
};
