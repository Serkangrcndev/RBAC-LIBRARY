import React from 'react';
import { Container, Typography, Grid, Card, CardContent, Avatar, Divider } from '@mui/material';
import './About.css';

export default function About() {
  // Kütüphane hakkında bilgiler
  const stats = [
    { title: '10,000+', description: 'Kitap Koleksiyonu' },
    { title: '1,200+', description: 'Aktif Üye' },
    { title: '24/7', description: 'Online Erişim' },
    { title: '50+', description: 'Yıllık Etkinlik' }
  ];

  // Ekip üyeleri
  const team = [
    {
      name: 'Ahmet Yılmaz',
      role: 'Kütüphane Müdürü',
      bio: '15 yıllık kütüphanecilik deneyimiyle koleksiyonumuzu yönetiyor.',
      avatar: 'https://randomuser.me/api/portraits/men/32.jpg'
    },
    {
      name: 'Ayşe Kaya',
      role: 'Dijital Arşiv Sorumlusu',
      bio: 'Dijital koleksiyonumuzun geliştirilmesi ve korunmasından sorumlu.',
      avatar: 'https://randomuser.me/api/portraits/women/44.jpg'
    },
    {
      name: 'Mehmet Demir',
      role: 'Etkinlik Koordinatörü',
      bio: 'Okuma grupları ve yazarlarla söyleşi etkinliklerini düzenliyor.',
      avatar: 'https://randomuser.me/api/portraits/men/67.jpg'
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

      {/* İstatistikler */}
      <div className="stats-section">
        <Container maxWidth="lg">
          <Grid container spacing={3} justifyContent="center">
            {stats.map((stat, index) => (
              <Grid item xs={6} md={3} key={index}>
                <div className="stat-card">
                  <Typography variant="h3" className="stat-number">
                    {stat.title}
                  </Typography>
                  <Typography variant="subtitle1" className="stat-description">
                    {stat.description}
                  </Typography>
                </div>
              </Grid>
            ))}
          </Grid>
        </Container>
      </div>

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
        <Grid container spacing={4} className="team-container">
          {team.map((member, index) => (
            <Grid item xs={12} md={4} key={index}>
              <Card className="team-card">
                <CardContent>
                  <Avatar
                    src={member.avatar}
                    alt={member.name}
                    className="team-avatar"
                  />
                  <Typography variant="h5" className="team-name">
                    {member.name}
                  </Typography>
                  <Typography variant="subtitle1" className="team-role">
                    {member.role}
                  </Typography>
                  <Typography variant="body2" className="team-bio">
                    {member.bio}
                  </Typography>
                </CardContent>
              </Card>
            </Grid>
          ))}
        </Grid>
      </Container>
    </div>
  );
} 