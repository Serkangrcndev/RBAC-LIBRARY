import React from 'react';
import { Container, Typography, Grid, Card, CardContent, Avatar, Divider } from '@mui/material';
import './About.css';
import serkanhalilImage from '../../assets/halil_serkan.jpg';
import burakImage from '../../assets/burak.jpeg';
import umutImage from '../../assets/umut.jpg';
import semihaImage from '../../assets/semiha.png';

export default function About() {
  const team = [
    {
      name: 'Semiha Bahçebaşı',
      role: 'Kod Kraliçesi / Proje Yöneticisi',
      bio: '“Bu feature dün çalışıyordu?” cümlesine hayatında yer vermeyen, Jira’dan önce bizden uyanan, ekibin ablası. Kafamız karışınca Google’dan önce Semiha’ya sorarız.',
      avatar: semihaImage
    },
    {
      name: 'Bekir Burak Saka',
      role: 'Sunucu Büyücüsü / Back-End Geliştirici',
      bio: 'Görünmeyen yerlerin kahramanı. Sunucu çökerse gözleri parlar çünkü “bi’ bakayım” diyip düzeltir. Kafası SQL, gönlü JSON.',
      avatar: burakImage
    },
    {
      name: 'Serkan Gürcan',
      role: 'Pixel Sihirbazı / Front-End Geliştirici',
      bio: 'Bir div’le dünyaları inşa eder. Tasarımcının hayalini, kullanıcıya tatlı tatlı sunar. CSS’te margin verince meditasyon yapmış gibi oluyor.',
      avatar: serkanhalilImage
    },
    {
      name: 'Halil Malatyalı',
      role: 'CSS Kâşifi / Front-End Geliştirici',
      bio: '“Bu neden hizalanmadı ya?” diyerek sabahlara kadar kod yazan efsane. Her bug’la küçük bir sinir krizi, her çözümle küçük bir zafer yaşar.',
      avatar: serkanhalilImage
    },
    {
      name: 'Umut Aydın',
      role: 'Kodun Jedi’si / Full-Stack Geliştirici',
      bio: 'Hem ön hem arka, gerekirse veritabanına da atlar. Kafası karışıkken bile sistem ayakta. En çok kullanılan cümlesi: “Bunu bi refactor edelim.”',
      avatar: umutImage
    }
  ];
  

  return (
    <div className="about-page">
      {/* Hero Section */}
      <div className="about-hero">
        <Container maxWidth="lg">
          <Typography variant="h2" className="about-hero-title">
            Kayseri Şeker Kütüphanesi
          </Typography>
          <Typography variant="h5" className="about-hero-subtitle">
            Bilgiye erişimin en modern yolu
          </Typography>
        </Container>
      </div>

      {/* Misyon ve Vizyon */}
      <Container maxWidth="lg" className="about-section">
        <Grid container spacing={4}>
          <Grid item xs={12} md={6}>
            <div className="about-card mission-card">
              <Typography variant="h4" className="section-title">
                Misyonumuz
              </Typography>
              <Typography variant="body1" className="section-content">
                Kitaplara ve bilgiye erişimi kolaylaştırmak, okuma alışkanlığını teşvik etmek ve toplumun her kesimine hitap eden zengin bir koleksiyon sunmak. Dijital ve fiziksel kaynaklarımızla bilgiye erişimde eşitlik sağlamak.
              </Typography>
            </div>
          </Grid>
          <Grid item xs={12} md={6}>
            <div className="about-card vision-card">
              <Typography variant="h4" className="section-title">
                Vizyonumuz
              </Typography>
              <Typography variant="body1" className="section-content">
                Modern teknoloji ile geleneksel kütüphanecilik anlayışını birleştirerek, bölgemizin en kapsamlı ve erişilebilir bilgi merkezi olmak. Sürekli yenilenen koleksiyonumuz ve yenilikçi hizmetlerimizle kullanıcılarımıza ilham vermek.
              </Typography>
            </div>
          </Grid>
        </Grid>
      </Container>


      {/* Tarihçe */}
      <Container maxWidth="lg" className="about-section">
        <Typography variant="h3" className="section-title text-center">
          Tarihçemiz
        </Typography>
        <Divider className="section-divider" />
        <div className="history-content">
          <Typography variant="body1" paragraph>
            Kayseri Şeker Kütüphanesi, 1985 yılında şirket çalışanlarının ve ailelerinin eğitim ihtiyaçlarını karşılamak amacıyla küçük bir koleksiyonla kuruldu. Zaman içinde koleksiyonumuz genişledi ve 2005 yılında halka açık bir kütüphane haline geldik.
          </Typography>
          <Typography variant="body1" paragraph>
            2015 yılında tamamen yenilenen binamız ve teknolojik altyapımızla modern bir bilgi merkezi olarak hizmet vermeye başladık. Bugün, 10.000'den fazla basılı kitap, binlerce e-kitap ve dijital kaynak, multimedya içerikler ve özel koleksiyonlarla hizmet vermekteyiz.
          </Typography>
          <Typography variant="body1">
            Düzenli olarak gerçekleştirdiğimiz okuma grupları, yazar söyleşileri, çocuk etkinlikleri ve eğitim programlarıyla toplumun her kesiminden insanları kütüphanemizde buluşturuyoruz.
          </Typography>
        </div>
      </Container>

      {/* Ekip */}
      <Container maxWidth="lg" className="about-section">
        <Typography variant="h3" className="section-title text-center">
          Ekibimiz
        </Typography>
        <Divider className="section-divider" />
        <div className="team-grid-container">
          {/* İlk satır - 1 kart (ortada) */}
          <div className="team-row team-row-single">
            <Card className="team-card">
              <CardContent>
                <Avatar
                  src={team[0].avatar}
                  alt={team[0].name}
                  className="team-avatar"
                />
                <Typography variant="h5" className="team-name">
                  {team[0].name}
                </Typography>
                <Typography variant="subtitle1" className="team-role">
                  {team[0].role}
                </Typography>
                <Typography variant="body2" className="team-bio">
                  {team[0].bio}
                </Typography>
              </CardContent>
            </Card>
          </div>
          
          {/* İkinci satır - 2 kart (yan yana) */}
          <div className="team-row team-row-double">
            <Card className="team-card">
              <CardContent>
                <Avatar
                  src={team[1].avatar}
                  alt={team[1].name}
                  className="team-avatar"
                />
                <Typography variant="h5" className="team-name">
                  {team[1].name}
                </Typography>
                <Typography variant="subtitle1" className="team-role">
                  {team[1].role}
                </Typography>
                <Typography variant="body2" className="team-bio">
                  {team[1].bio}
                </Typography>
              </CardContent>
            </Card>
            <Card className="team-card">
              <CardContent>
                <Avatar
                  src={team[2].avatar}
                  alt={team[2].name}
                  className="team-avatar"
                />
                <Typography variant="h5" className="team-name">
                  {team[2].name}
                </Typography>
                <Typography variant="subtitle1" className="team-role">
                  {team[2].role}
                </Typography>
                <Typography variant="body2" className="team-bio">
                  {team[2].bio}
                </Typography>
              </CardContent>
            </Card>
          </div>
          
          {/* Üçüncü satır - 2 kart (yan yana) */}
          <div className="team-row team-row-double">
            <Card className="team-card">
              <CardContent>
                <Avatar
                  src={team[3].avatar}
                  alt={team[3].name}
                  className="team-avatar"
                />
                <Typography variant="h5" className="team-name">
                  {team[3].name}
                </Typography>
                <Typography variant="subtitle1" className="team-role">
                  {team[3].role}
                </Typography>
                <Typography variant="body2" className="team-bio">
                  {team[3].bio}
                </Typography>
              </CardContent>
            </Card>
            <Card className="team-card">
              <CardContent>
                <Avatar
                  src={team[4].avatar}
                  alt={team[4].name}
                  className="team-avatar"
                />
                <Typography variant="h5" className="team-name">
                  {team[4].name}
                </Typography>
                <Typography variant="subtitle1" className="team-role">
                  {team[4].role}
                </Typography>
                <Typography variant="body2" className="team-bio">
                  {team[4].bio}
                </Typography>
              </CardContent>
            </Card>
          </div>
        </div>
      </Container>
    </div>
  );
} 